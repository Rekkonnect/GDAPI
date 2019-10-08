using GDAPI.Utilities.Objects.General.Music;
using GDAPI.Utilities.Objects.General.TimingPoints;
using GDAPI.Utilities.Objects.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.General;
using GDAPI.Utilities.Objects.Presets.GuidelineEditor;
using NAudio.Wave;
using System.Collections.Generic;
using System.IO;

namespace GDAPI.Application.Editors
{
    /// <summary>Provides tools to edit <seealso cref="Guideline"/>s in a level.</summary>
    public class GuidelineEditor
    {
        private List<GuidelineEditorPresetEvent> selectedEvents;

        /// <summary>The level whose <seealso cref="Guideline"/>s to edit.</summary>
        public readonly Level Level;
        /// <summary>The <seealso cref="GuidelineEditorPreset"/> that is being edited and will be used in guideline editing operations.</summary>
        public GuidelineEditorPreset Preset;

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditor"/> class.</summary>
        /// <param name="level">The level whose <seealso cref="Guideline"/>s to edit.</param>
        /// <param name="preset">The <seealso cref="GuidelineEditorPreset"/> that is to be edited and will be used in guideline editing operations.</param>
        public GuidelineEditor(Level level, GuidelineEditorPreset preset)
        {
            Level = level;
            Preset = preset;
        }

        /// <summary>Clones the currently selected events in the <seealso cref="GuidelineEditorPreset"/>, adds them to separate tracks and replaces the current event selection to the newly cloned events.</summary>
        public void CloneSelectedEvents()
        {
            var clonedEvents = new List<GuidelineEditorPresetEvent>(selectedEvents.Count);
            var newTracks = new GuidelineEditorPresetTrack[Preset.Tracks.Count];

            for (int i = 0; i < newTracks.Length; i++)
                newTracks[i] = new GuidelineEditorPresetTrack();

            foreach (var e in selectedEvents)
                for (int i = 0; i < Preset.Tracks.Count; i++)
                    if (Preset.Tracks[i].Contains(e))
                    {
                        var cloned = e.Clone();
                        newTracks[i].Add(cloned);
                        clonedEvents.Add(cloned);
                        break;
                    }

            var tracksToAdd = new List<GuidelineEditorPresetTrack>(newTracks.Length);
            foreach (var t in newTracks)
                if (t.EventCount > 0)
                    tracksToAdd.Add(t);
            Preset.Tracks.AddRange(tracksToAdd);

            selectedEvents = clonedEvents;
        }

        /// <summary>Deletes the currently selected events from the <seealso cref="GuidelineEditorPreset"/>.</summary>
        public void DeleteSelectedEvents()
        {
            foreach (var e in selectedEvents)
                foreach (var t in Preset.Tracks)
                    if (t.Remove(e))
                        break;
            selectedEvents.Clear();
        }

        /// <summary>Extends the selected events' duration by an amount.</summary>
        /// <param name="amount">The amount to extend the selected events' duration by.</param>
        public void ExtendSelectedEventsDuration(MeasuredDuration amount)
        {
            // Extend the events by the amount
            foreach (var e in selectedEvents)
            {
                var eventTimeSignature = Preset.TimingPoints.TimingPointAtTime(e.TimePosition).TimeSignature;

                var initialEnd = e.GetEnd(eventTimeSignature);
                var initialTimingPoint = Preset.TimingPoints.TimingPointAtTime(initialEnd);
                int initialTimingPointIndex = Preset.TimingPoints.IndexOf(initialTimingPoint);

                // Find the timing point that the new event end will be at
                var newTimingPoint = Preset.TimingPoints.TimingPointAtTime(initialEnd.Add(amount, eventTimeSignature));
                int newTimingPointIndex = Preset.TimingPoints.IndexOf(newTimingPoint);

                // Ensure that the event can be extended further, since it can only pass through multiple relative timing points with the same time signature
                int finalValidTimingPoint = initialTimingPointIndex;
                for (int i = initialTimingPointIndex + 1; i <= newTimingPointIndex; i++)
                {
                    var point = Preset.TimingPoints[i];

                    if (point is AbsoluteTimingPoint)
                        break;
                    if (point.TimeSignature != eventTimeSignature)
                        break;

                    finalValidTimingPoint = i;
                }

                if (finalValidTimingPoint == newTimingPointIndex)
                    e.Duration.IncreaseValue(amount, eventTimeSignature);
                else
                    e.Duration = e.TimePosition.DistanceFrom(Preset.TimingPoints[finalValidTimingPoint + 1].GetRelativeTimePosition(), eventTimeSignature);
            }
        }

        /// <summary>Generates guidelines based on the current <seealso cref="GuidelineEditorPreset"/> and adds them to the level.</summary>
        /// <param name="appendGuidelines">If <see langword="true"/>, the generated guidelines will be appended to the level and the previous ones will be preserved, otherwise the new ones will replace the old ones.</param>
        public void GenerateGuidelines(bool appendGuidelines = true)
        {
            var guidelines = new GuidelineCollection();

            double audioLength = GetMP3Duration(Level.CustomSongLocation);
            var tracks = Preset.Tracks.CompactTracks(Preset.TimingPoints);

            var startingTimingPoint = Preset.TimingPoints.TimingPointAtTime(Preset.StartingPosition);
            int startingTimingPointIndex = Preset.TimingPoints.IndexOf(startingTimingPoint);

            foreach (var t in tracks)
            {
                var currentPosition = MeasuredTimePosition.Start;
                var currentTimingPoint = startingTimingPoint;
                int currentTimingPointIndex = startingTimingPointIndex;
                foreach (var e in t)
                {
                    currentPosition = e.TimePosition;

                    // Determine when to end guideline creation
                    if (Preset.IsEndingPositionEndOfSong)
                        if (currentTimingPoint.ConvertTime(currentPosition).TotalSeconds > audioLength)
                            break;
                    else if (currentPosition > Preset.EndingPosition)
                        break;

                    // During the event, the timing point may only change under the same time signature
                    // Therefore, precalculation of the ending position is acceptable
                    var eventEnd = e.GetEnd(currentTimingPoint.TimeSignature);

                    foreach (var m in e.EventPatternInfo.Pattern.Measures)
                    {
                        bool shouldBreak = false;
                        foreach (var n in m.Notes)
                        {
                            currentPosition.BeatWithFraction = n.Position.BeatWithFraction;

                            if (shouldBreak = currentPosition > eventEnd)
                                break;

                            // Determine the current timing point to use
                            while (Preset.TimingPoints[currentTimingPointIndex + 1].GetRelativeTimePosition() < currentPosition)
                                currentTimingPointIndex++;
                            currentTimingPoint = Preset.TimingPoints[currentTimingPointIndex];

                            guidelines.Add(new Guideline(currentTimingPoint.ConvertTime(currentPosition).TotalSeconds, n.Color));
                        }

                        if (shouldBreak)
                            break;

                        currentPosition.AdvanceToStartOfNextMeasure();
                    }
                }
            }

            Level.AddGuidelines(guidelines, appendGuidelines);
        }

        private static double GetMP3Duration(string fileName)
        {
            double duration = 0;
            using (var fs = File.OpenRead(fileName))
            {
                Mp3Frame frame;
                for (LoadFrame(); frame != null; LoadFrame())
                {
                    double toAdd = frame.SampleCount * 2.0 / frame.SampleRate;

                    if (frame.ChannelMode != ChannelMode.Mono)
                        toAdd *= 2;

                    duration += toAdd;
                }
                void LoadFrame() => frame = Mp3Frame.LoadFromStream(fs);
            }
            return duration;
        }
    }
}
