using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Objects.General.Music;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Convert;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Represents a preset pattern for the guideline editor.</summary>
    public class GuidelineEditorPresetPattern
    {
        // TODO: Consider removing BPM and time signature from pattern.
        //       This logic should be handled in the preset timing points.

        /// <summary>The name of the pattern.</summary>
        public string Name;
        /// <summary>The BPM of the pattern.</summary>
        public BPM BPM;
        /// <summary>The time signature of the pattern.</summary>
        public TimeSignature TimeSignature;
        /// <summary>The measures of the pattern.</summary>
        public List<GuidelineEditorPresetMeasure> Measures;

        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetPattern"/> class.</summary>
        public GuidelineEditorPresetPattern() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetPattern"/> class.</summary>
        /// <param name="name">The name of the pattern.</param>
        /// <param name="bpm">The BPM of the pattern.</param>
        /// <param name="timeSignature">The time signature of the pattern.</param>
        /// <param name="measures">The measures of the pattern.</param>
        public GuidelineEditorPresetPattern(string name, BPM bpm, (int, int) timeSignature, List<GuidelineEditorPresetMeasure> measures)
            : this(name, bpm, new TimeSignature(timeSignature), measures) { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetPattern"/> class.</summary>
        /// <param name="name">The name of the pattern.</param>
        /// <param name="bpm">The BPM of the pattern.</param>
        /// <param name="timeSignature">The time signature of the pattern.</param>
        /// <param name="measures">The measures of the pattern.</param>
        public GuidelineEditorPresetPattern(string name, BPM bpm, TimeSignature timeSignature, List<GuidelineEditorPresetMeasure> measures)
        {
            Name = name;
            BPM = bpm;
            TimeSignature = timeSignature;
            Measures = measures;
        }

        /// <summary>Clones this instance of <seealso cref="GuidelineEditorPresetPattern"/>.</summary>
        public GuidelineEditorPresetPattern Clone() => new GuidelineEditorPresetPattern(Name, BPM, TimeSignature, Measures.Clone());

        /// <summary>Parses a <seealso cref="GuidelineEditorPresetPattern"/> from raw data with a specified name.</summary>
        /// <param name="rawData">The raw data of the pattern that will be parsed.</param>
        public static GuidelineEditorPresetPattern Parse(string rawData)
        {
            int nameStartIndex = 1;
            int nameEndIndex = 2;
            while (rawData[nameEndIndex] != '"')
                nameEndIndex++;
            string name = rawData.Substring(nameStartIndex, nameEndIndex - nameStartIndex);

            string[] rawPatternData = rawData.Substring(nameEndIndex + 2).Split('|');
            var bpm = ToDouble(rawPatternData[0]);
            var timeSignature = TimeSignature.Parse(rawPatternData[1]);
            var measures = rawPatternData.Last().Split(';').ToList().ConvertAll(m => GuidelineEditorPresetMeasure.Parse(m));

            return new GuidelineEditorPresetPattern(name, bpm, timeSignature, measures);
        }

        /// <summary>Gets the string representation of this pattern that will be used for saving the contents in a file.</summary>
        public override string ToString()
        {
            var result = new StringBuilder($"\"{Name}\"|{BPM}|{TimeSignature}|");
            foreach (var m in Measures)
                result.Append($"{m};");
            return result.RemoveLastIfEndsWith(';').ToString();
        }
    }
}