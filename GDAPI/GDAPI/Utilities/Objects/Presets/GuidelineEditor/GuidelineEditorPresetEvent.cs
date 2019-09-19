using GDAPI.Utilities.Objects.General.Music;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Represents an event in the <seealso cref="GuidelineEditorPreset"/>.</summary>
    public class GuidelineEditorPresetEvent
    {
        /// <summary>The measured time position of the event.</summary>
        public MeasuredTimePosition TimePosition;
        /// <summary>The duration of the event.</summary>
        public MeasuredDuration Duration;
        /// <summary>Contains information about the preset that's used.</summary>
        public GuidelineEditorPresetPatternEventInfo EventInfo;
        /// <summary>Determines whether this event forces a new measure.</summary>
        public bool ForceNewMeasure;

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetPatternEventInfo"/> class from a given time position and pattern. The event's duration defaults to the pattern's measure count.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="pattern">The pattern that this event will use.</param>
        /// <param name="forceNewMeasure">Determines whether this event forces a new measure.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetPattern pattern, bool forceNewMeasure = false)
            : this(timePosition, new GuidelineEditorPresetPatternEventInfo(pattern), new MeasuredDuration(pattern.Measures.Count), forceNewMeasure) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetPatternEventInfo"/> class from a given time position, pattern and duration.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="pattern">The pattern that this event will use.</param>
        /// <param name="duration">The duration of the event.</param>
        /// <param name="forceNewMeasure">Determines whether this event forces a new measure.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetPattern pattern, MeasuredDuration duration, bool forceNewMeasure = false)
            : this(timePosition, new GuidelineEditorPresetPatternEventInfo(pattern), duration, forceNewMeasure) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetPatternEventInfo"/> class from a given time position and event info. The event's duration defaults to the pattern's measure count.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="eventInfo">The <seealso cref="GuidelineEditorPresetPatternEventInfo"/> to use in the event.</param>
        /// <param name="forceNewMeasure">Determines whether this event forces a new measure.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetPatternEventInfo eventInfo, bool forceNewMeasure = false)
            : this(timePosition, eventInfo, new MeasuredDuration(eventInfo.Pattern.Measures.Count), forceNewMeasure) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetPatternEventInfo"/> class from a given time position, event info and duration.</summary>
        /// <param name="timePosition">The starting time position of the pattern.</param>
        /// <param name="eventInfo">The <seealso cref="GuidelineEditorPresetPatternEventInfo"/> to use in the event.</param>
        /// <param name="duration">The duration of the event.</param>
        /// <param name="forceNewMeasure">Determines whether this event forces a new measure.</param>
        public GuidelineEditorPresetEvent(MeasuredTimePosition timePosition, GuidelineEditorPresetPatternEventInfo eventInfo, MeasuredDuration duration, bool forceNewMeasure = false)
        {
            TimePosition = timePosition;
            Duration = duration;
            EventInfo = eventInfo;
            ForceNewMeasure = forceNewMeasure;
        }
    }
}
