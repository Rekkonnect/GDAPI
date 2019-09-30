using GDAPI.Utilities.Functions.Extensions;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Contains information about an event track in a <seealso cref="GuidelineEditorPresetPattern"/>.</summary>
    public class GuidelineEditorPresetTrack
    {
        /// <summary>The events of the track.</summary>
        public List<GuidelineEditorPresetEvent> Events;

        /// <summary>Gets the unique patterns that are used in this track.</summary>
        public HashSet<GuidelineEditorPresetPattern> UniquePatterns => new HashSet<GuidelineEditorPresetPattern>(Events.ConvertAll(e => e.EventPatternInfo.Pattern));

        /// <summary>Creates a new empty instance of the <seealso cref="GuidelineEditorPresetTrack"/> class.</summary>
        public GuidelineEditorPresetTrack() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetTrack"/> class.</summary>
        /// <param name="events">The list of colors to use for this measure.</param>
        public GuidelineEditorPresetTrack(List<GuidelineEditorPresetEvent> events)
        {
            Events = events;
        }

        /// <summary>Clones this instance and returns the new instance.</summary>
        public GuidelineEditorPresetTrack Clone()
        {
            var n = new List<GuidelineEditorPresetEvent>();
            for (int i = 0; i < Events.Count; i++)
                n.Add(Events[i]);
            return new GuidelineEditorPresetTrack(n);
        }

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

        /// <summary>Gets the string representation of this <seealso cref="GuidelineEditorPresetTrack"/> that will be used in the preset data.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var e in Events)
                result.AppendLine(e.ToString());
            return result.RemoveLastOrNone().ToString();
        }
    }
}
