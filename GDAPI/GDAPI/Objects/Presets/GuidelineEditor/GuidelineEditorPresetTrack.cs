using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.DataStructures;
using GDAPI.Objects.Music;
using GDAPI.Objects.TimingPoints;

namespace GDAPI.Objects.Presets.GuidelineEditor
{
    /// <summary>Contains information about an event track in a <seealso cref="GuidelineEditorPresetPattern"/>.</summary>
    public class GuidelineEditorPresetTrack : IEnumerable<GuidelineEditorPresetEvent>
    {
        /// <summary>The events of the track.</summary>
        private readonly SortedList<GuidelineEditorPresetEvent> events;

        /// <summary>Gets the unique patterns that are used in this track.</summary>
        public HashSet<GuidelineEditorPresetPattern> UniquePatterns => new(events.Select(e => e.EventPatternInfo.Pattern));
        /// <summary>Gets the event count in this track.</summary>
        public int EventCount => events.Count;

        /// <summary>Creates a new empty instance of the <seealso cref="GuidelineEditorPresetTrack"/> class.</summary>
        public GuidelineEditorPresetTrack() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetTrack"/> class.</summary>
        /// <param name="e">The event to initialize the track with.</param>
        public GuidelineEditorPresetTrack(GuidelineEditorPresetEvent e)
        {
            events = new SortedList<GuidelineEditorPresetEvent> { e };
        }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetTrack"/> class.</summary>
        /// <param name="trackEvents">The events of the track.</param>
        public GuidelineEditorPresetTrack(IEnumerable<GuidelineEditorPresetEvent> trackEvents)
        {
            events = new SortedList<GuidelineEditorPresetEvent>(trackEvents);
        }

        /// <summary>Adds an event to the track.</summary>
        /// <param name="e">The event to add to the track.</param>
        public void Add(GuidelineEditorPresetEvent e) => events.Add(e);
        /// <summary>Remove an event from the track.</summary>
        /// <param name="e">The event to remove from the track.</param>
        public bool Remove(GuidelineEditorPresetEvent e) => events.Remove(e);
        /// <summary>Clears the track by removing all of its events.</summary>
        public void Clear() => events.Clear();

        /// <summary>Determines whether an <seealso cref="GuidelineEditorPresetEvent"/> is contained in the track.</summary>
        /// <param name="e">The <seealso cref="GuidelineEditorPresetEvent"/> to check whether it is contained in the track.</param>
        public bool Contains(GuidelineEditorPresetEvent e) => events.BinarySearch(e) > -1;
        /// <summary>Gets the index of the specified <seealso cref="GuidelineEditorPresetEvent"/> in this track's event list.</summary>
        /// <param name="e">The <seealso cref="GuidelineEditorPresetEvent"/> to get the index of in this track's event list.</param>
        public int IndexOf(GuidelineEditorPresetEvent e) => events.BinarySearch(e);

        /// <summary>Clones this instance and returns the new instance.</summary>
        public GuidelineEditorPresetTrack Clone()
        {
            var n = new List<GuidelineEditorPresetEvent>();
            for (int i = 0; i < events.Count; i++)
                n.Add(events[i]);
            return new GuidelineEditorPresetTrack(n);
        }

        /// <summary>Gets the end time position of the track based on the <seealso cref="TimingPointList"/> of the <seealso cref="GuidelineEditorPreset"/>.</summary>
        /// <param name="timingPoints">The <seealso cref="TimingPointList"/> of the <seealso cref="GuidelineEditorPreset"/>.</param>
        public MeasuredTimePosition GetEnd(TimingPointList timingPoints)
        {
            var max = events.Maximum;
            return max.GetEnd(timingPoints.TimingPointAtTime(max.TimePosition).TimeSignature);
        }
        /// <summary>Gets the duration of the track based on the <seealso cref="TimingPointList"/> of the <seealso cref="GuidelineEditorPreset"/>.</summary>
        /// <param name="timingPoints">The <seealso cref="TimingPointList"/> of the <seealso cref="GuidelineEditorPreset"/>.</param>
        public MeasuredDuration GetTrackDuration(TimingPointList timingPoints) => new(GetEnd(timingPoints));

        /// <summary>Gets the event of this track at the specified index.</summary>
        /// <param name="index">The index of the event to get.</param>
        public GuidelineEditorPresetEvent this[int index] => events[index];

        /// <summary>Parses a <seealso cref="GuidelineEditorPresetTrack"/> from raw data.</summary>
        /// <param name="rawData">The raw data of the <seealso cref="GuidelineEditorPresetTrack"/> that will be parsed.</param>
        public static GuidelineEditorPresetTrack Parse(string rawData, List<GuidelineEditorPresetPattern> patternPool)
        {
            var lines = rawData.Split('\n');
            var events = new List<GuidelineEditorPresetEvent>(lines.Length);
            foreach (var l in lines)
                events.Add(GuidelineEditorPresetEvent.Parse(l, patternPool));
            return new GuidelineEditorPresetTrack(events);
        }

        public IEnumerator<GuidelineEditorPresetEvent> GetEnumerator() => events.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => events.GetEnumerator();

        /// <summary>Gets the string representation of this <seealso cref="GuidelineEditorPresetTrack"/> that will be used in the preset data.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var e in events)
                result.AppendLine(e.ToString());
            return result.RemoveLastOrNone().ToString();
        }
    }
}
