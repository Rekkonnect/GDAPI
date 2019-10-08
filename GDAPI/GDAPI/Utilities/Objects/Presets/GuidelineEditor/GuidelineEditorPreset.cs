using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Objects.General.Music;
using GDAPI.Utilities.Objects.General.TimingPoints;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Convert;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Represents a preset for the guideline editor.</summary>
    public class GuidelineEditorPreset : Preset
    {
        /// <summary>The name of the preset.</summary>
        public string Name;
        /// <summary>The offset, in seconds, of the preset.</summary>
        public double Offset;
        /// <summary>The starting position from which to generate the guidelines.</summary>
        public MeasuredTimePosition StartingPosition = MeasuredTimePosition.Start;
        /// <summary>The ending position at which to stop generating the guidelines.</summary>
        public MeasuredTimePosition EndingPosition = MeasuredTimePosition.UnknownEnd;

        /// <summary>Gets a copy of the list of tracks of this instance.</summary>
        public GuidelineEditorPresetTrackList Tracks = new GuidelineEditorPresetTrackList();
        /// <summary>Gets the unique patterns that are used in this track.</summary>
        public HashSet<GuidelineEditorPresetPattern> UniquePatterns => Tracks.UniquePatterns;
        /// <summary>The timing points of the preset.</summary>
        public TimingPointList TimingPoints { get; } = new TimingPointList();

        /// <summary>Determines whether the preset's ending position is the end of song, regardless of its measures.</summary>
        public bool IsEndingPositionEndOfSong => EndingPosition == MeasuredTimePosition.UnknownEnd;

        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        public GuidelineEditorPreset()
            : this(null, 0, MeasuredTimePosition.Start, MeasuredTimePosition.UnknownEnd) { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        /// <param name="name">The name of the preset.</param>
        public GuidelineEditorPreset(string name)
            : this(name, 0, MeasuredTimePosition.Start, MeasuredTimePosition.UnknownEnd) { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        /// <param name="name">The name of the preset.</param>
        /// <param name="offset">The offset, in seconds, of the preset.</param>
        /// <param name="start">The starting position from which to generate the guidelines.</param>
        /// <param name="end">The ending position at which to stop generating the guidelines.</param>
        public GuidelineEditorPreset(string name, double offset, MeasuredTimePosition start, MeasuredTimePosition end)
        {
            Name = name;
            Offset = offset;
            StartingPosition = start;
            EndingPosition = end;
        }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        /// <param name="name">The name of the preset.</param>
        /// <param name="offset">The offset, in seconds, of the preset.</param>
        /// <param name="start">The starting position from which to generate the guidelines.</param>
        /// <param name="end">The ending position at which to stop generating the guidelines.</param>
        /// <param name="startingPoint">The starting <seealso cref="AbsoluteTimingPoint"/> of the preset.</param>
        public GuidelineEditorPreset(string name, double offset, MeasuredTimePosition start, MeasuredTimePosition end, AbsoluteTimingPoint startingPoint)
            : this(name, offset, start, end)
        {
            TimingPoints.Add(startingPoint);
        }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        /// <param name="name">The name of the preset.</param>
        /// <param name="offset">The offset, in seconds, of the preset.</param>
        /// <param name="start">The starting position from which to generate the guidelines.</param>
        /// <param name="end">The ending position at which to stop generating the guidelines.</param>
        /// <param name="presetTracks">The tracks of the preset.</param>
        /// <param name="presetTimingPoints">The timing points of the preset.</param>
        public GuidelineEditorPreset(string name, double offset, MeasuredTimePosition start, MeasuredTimePosition end, GuidelineEditorPresetTrackList presetTracks, TimingPointList presetTimingPoints)
            : this(name, offset, start, end)
        {
            Tracks = presetTracks.Clone();
            TimingPoints = presetTimingPoints;
        }

        /// <summary>Clones this instance of <seealso cref="GuidelineEditorPreset"/>.</summary>
        public GuidelineEditorPreset Clone() => new GuidelineEditorPreset(Name, Offset, StartingPosition, EndingPosition, Tracks, TimingPoints);

        /// <summary>Parses a <seealso cref="GuidelineEditorPreset"/> from raw data wtih a specified name.</summary>
        /// <param name="name">The name of the preset.</param>
        /// <param name="rawData">The raw data of the preset that will be parsed.</param>
        public static GuidelineEditorPreset Parse(string name, string rawData)
        {
            var patternPool = new List<GuidelineEditorPresetPattern>();
            var tracks = new GuidelineEditorPresetTrackList();
            var timingPoints = new TimingPointList();

            var lines = rawData.GetLines().ToList();
            lines.RemoveAll(s => s.StartsWith("//"));
            int currentLineIndex = 0;
            string currentLine;

            // Pattern pool
            for (; (currentLine = lines[currentLineIndex]).Length > 0; currentLineIndex++)
            {
                patternPool.Add(GuidelineEditorPresetPattern.Parse(currentLine));
                currentLineIndex++;
            }

            // Intermediate parameters
            currentLineIndex++;
            var split = lines[currentLineIndex].Split('|');
            var offset = ToDouble(split[0]);
            var start = MeasuredTimePosition.Parse(split[1]);
            var end = MeasuredTimePosition.Parse(split[2]);
            currentLineIndex++;

            // Timing points
            for (; (currentLine = lines[currentLineIndex]).Length > 0; currentLineIndex++)
            {
                timingPoints.Add(TimingPoint.Parse(currentLine));
                currentLineIndex++;
            }

            // Tracks
            for (currentLineIndex++; currentLineIndex < lines.Count; currentLineIndex++)
            {
                // TODO: Consider caring about the track indices
                var events = new List<GuidelineEditorPresetEvent>();
                for (currentLineIndex++; currentLineIndex < lines.Count && (currentLine = lines[currentLineIndex]).Length > 0; currentLineIndex++)
                    events.Add(GuidelineEditorPresetEvent.Parse(currentLine, patternPool));
                tracks.Add(new GuidelineEditorPresetTrack(events));
            }

            return new GuidelineEditorPreset(name, offset, start, end, tracks, timingPoints);
        }

        /// <summary>Gets the string representation of this preset type that will be used for saving the contents in a file.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            var uniquePatterns = UniquePatterns;
            foreach (var p in uniquePatterns)
                result.AppendLine(p.ToString());

            result.AppendLine($"{Offset}|{StartingPosition}|{EndingPosition}").AppendLine();

            result.AppendLine(TimingPoints.ToString()); // TODO: Create new class called GuidelineEditorPresetTimingPointList, whose ToString should be used
            result.AppendLine(Tracks.ToString());

            return result.RemoveLastIfEndsWith('\n').ToString();
        }
    }
}