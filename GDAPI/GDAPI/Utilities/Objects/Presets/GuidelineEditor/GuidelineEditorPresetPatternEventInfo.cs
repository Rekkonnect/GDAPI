using GDAPI.Utilities.Objects.General.Music;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Contains information about the pattern that is used in the <seealso cref="GuidelineEditorPresetEvent"/>.</summary>
    public class GuidelineEditorPresetPatternEventInfo
    {
        /// <summary>The pattern that will be used.</summary>
        public GuidelineEditorPresetPattern Pattern;
        /// <summary>The starting time position of the pattern.</summary>
        public MeasuredTimePosition PatternStart;

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetPatternEventInfo"/> class.</summary>
        /// <param name="pattern">The pattern that this event will use.</param>
        public GuidelineEditorPresetPatternEventInfo(GuidelineEditorPresetPattern pattern)
            : this(pattern, MeasuredTimePosition.Start) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetPatternEventInfo"/> class.</summary>
        /// <param name="pattern">The pattern that this event will use.</param>
        /// <param name="patternStart">The starting time position of the pattern.</param>
        public GuidelineEditorPresetPatternEventInfo(GuidelineEditorPresetPattern pattern, MeasuredTimePosition patternStart)
        {
            Pattern = pattern;
            PatternStart = patternStart;
        }
    }
}
