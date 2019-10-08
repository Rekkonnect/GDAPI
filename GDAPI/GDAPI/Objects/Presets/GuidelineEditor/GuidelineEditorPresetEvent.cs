using System;
using System.Collections.Generic;
using GDAPI.Objects.Music;

namespace GDAPI.Objects.Presets.GuidelineEditor
{
    /// <summary>Represents an event in the <seealso cref="GuidelineEditorPreset"/>.</summary>
    public class GuidelineEditorPresetEvent : IComparable<GuidelineEditorPresetEvent>
    {
        /// <summary>The measured time position of the event.</summary>
        public MeasuredTimePosition TimePosition;
        /// <summary>The duration of the event.</summary>
        public MeasuredDuration Duration;
        /// <summary>Contains information about the preset that's used.</summary>
        public GuidelineEditorPresetEventPatternInfo EventPatternInfo;

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetEvent"/> class from a given time position and pattern. The event's duration defaults to the pattern's measure count.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="pattern">The pattern that this event will use.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetPattern pattern)
            : this(timePosition, new GuidelineEditorPresetEventPatternInfo(pattern), new MeasuredDuration(pattern.Measures.Count)) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetEvent"/> class from a given time position, pattern and duration.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="pattern">The pattern that this event will use.</param>
        /// <param name="duration">The duration of the event.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetPattern pattern, MeasuredDuration duration)
            : this(timePosition, new GuidelineEditorPresetEventPatternInfo(pattern), duration) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetEvent"/> class from a given time position and event pattern info. The event's duration defaults to the pattern's measure count.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="eventPatternInfo">The <seealso cref="GuidelineEditorPresetEventPatternInfo"/> to use in the event.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetEventPatternInfo eventPatternInfo)
            : this(timePosition, eventPatternInfo, new MeasuredDuration(eventPatternInfo.Pattern.Measures.Count)) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetEvent"/> class from a given time position, event pattern info and duration.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="eventPatternInfo">The <seealso cref="GuidelineEditorPresetEventPatternInfo"/> to use in the event.</param>
        /// <param name="duration">The duration of the event.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetEventPatternInfo eventPatternInfo, MeasuredDuration duration)
        {
            TimePosition = timePosition;
            Duration = duration;
            EventPatternInfo = eventPatternInfo;
        }

        /// <summary>Gets the end time position of this event based on the used <seealso cref="TimeSignature"/>.</summary>
        /// <param name="timeSignature">The <seealso cref="TimeSignature"/> based on which to calculate the end time position of the event.</param>
        public MeasuredTimePosition GetEnd(TimeSignature timeSignature) => TimePosition.Add(Duration, timeSignature);

        /// <summary>Clones this <seealso cref="GuidelineEditorPresetEvent"/> instance and returns the new instance.</summary>
        public GuidelineEditorPresetEvent Clone() => new GuidelineEditorPresetEvent(TimePosition, EventPatternInfo.Clone(), Duration);

        /// <summary>Compares this <seealso cref="GuidelineEditorPresetEvent"/>'s time position with another's.</summary>
        /// <param name="other">The other <seealso cref="GuidelineEditorPresetEvent"/> whose time position to compare this event's to.</param>
        public int CompareTo(GuidelineEditorPresetEvent other) => TimePosition.CompareTo(other.TimePosition);

        /// <summary>Parses a <seealso cref="GuidelineEditorPresetEvent"/> from raw data.</summary>
        /// <param name="s">The raw data of the event that will be parsed.</param>
        /// <param name="patternPool">The pattern pool from which to retrieve the pattern object that is referred.</param>
        public static GuidelineEditorPresetEvent Parse(string s, List<GuidelineEditorPresetPattern> patternPool)
        {
            var split = s.Split(", ");
            var position = MeasuredTimePosition.Parse(split[0]);
            var duration = MeasuredDuration.Parse(split[1]);
            var eventInfo = GuidelineEditorPresetEventPatternInfo.Parse($"{split[2]}, {split[3]}", patternPool);
            return new GuidelineEditorPresetEvent(position, eventInfo, duration);
        }

        /// <summary>Gets the string representation of this event that will be used for saving the contents in a file.</summary>
        public override string ToString() => $"{TimePosition}, {Duration}, {EventPatternInfo}";
    }
}
