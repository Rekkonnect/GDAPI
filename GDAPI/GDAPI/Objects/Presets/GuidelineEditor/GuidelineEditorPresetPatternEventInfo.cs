using System.Collections.Generic;
using GDAPI.Objects.Music;

namespace GDAPI.Objects.Presets.GuidelineEditor
{
    /// <summary>Contains information about the pattern that is used in the <seealso cref="GuidelineEditorPresetEvent"/>.</summary>
    public class GuidelineEditorPresetEventPatternInfo
    {
        /// <summary>The pattern that will be used.</summary>
        public GuidelineEditorPresetPattern Pattern;
        /// <summary>The starting time position of the pattern.</summary>
        public MeasuredTimePosition PatternStart;

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetEventPatternInfo"/> class.</summary>
        /// <param name="pattern">The pattern that this event will use.</param>
        public GuidelineEditorPresetEventPatternInfo(GuidelineEditorPresetPattern pattern)
            : this(pattern, MeasuredTimePosition.Start) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineEditorPresetEventPatternInfo"/> class.</summary>
        /// <param name="pattern">The pattern that this event will use.</param>
        /// <param name="patternStart">The starting time position of the pattern.</param>
        public GuidelineEditorPresetEventPatternInfo(GuidelineEditorPresetPattern pattern, MeasuredTimePosition patternStart)
        {
            Pattern = pattern;
            PatternStart = patternStart;
        }

        /// <summary>Clones this <seealso cref="GuidelineEditorPresetEventPatternInfo"/> instance and returns the new instance.</summary>
        public GuidelineEditorPresetEventPatternInfo Clone() => new GuidelineEditorPresetEventPatternInfo(Pattern, PatternStart);

        /// <summary>Parses a <seealso cref="GuidelineEditorPresetEventPatternInfo"/> from raw data.</summary>
        /// <param name="s">The raw data of the pattern that will be parsed.</param>
        /// <param name="patternPool">The pattern pool from which to retrieve the pattern object that is referred.</param>
        public static GuidelineEditorPresetEventPatternInfo Parse(string s, List<GuidelineEditorPresetPattern> patternPool)
        {
            var split = s.Split(", ");
            var pattern = patternPool.Find(p => p.Name == split[0]);
            var start = MeasuredTimePosition.Parse(split[1]);
            return new GuidelineEditorPresetEventPatternInfo(pattern, start);
        }

        /// <summary>Gets the string representation of this event pattern info that will be used for saving the contents in a file.</summary>
        public override string ToString() => $"{Pattern.Name}, {PatternStart}";
    }
}
