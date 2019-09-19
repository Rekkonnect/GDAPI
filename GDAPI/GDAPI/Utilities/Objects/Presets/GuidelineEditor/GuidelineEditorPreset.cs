using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.General.Music;
using GDAPI.Utilities.Objects.GeometryDash.General;
using System.Collections.Generic;
using System.Text;
using static System.Convert;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Represents a preset for the guideline editor.</summary>
    public class GuidelineEditorPreset : Preset
    {
        /// <summary>The name of the preset.</summary>
        public string Name;
        /// <summary>The BPM of the preset.</summary>
        public double BPM;
        /// <summary>The time signature of the preset.</summary>
        public TimeSignature TimeSignature;
        /// <summary>The measures of the preset.</summary>
        public List<GuidelineEditorPresetMeasure> Measures;

        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        public GuidelineEditorPreset() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        /// <param name="name">The name of the preset.</param>
        /// <param name="bpm">The BPM of the preset.</param>
        /// <param name="timeSignature">The time signature of the preset.</param>
        /// <param name="measures">The measures of the preset.</param>
        public GuidelineEditorPreset(string name, double bpm, (int, int) timeSignature, List<GuidelineEditorPresetMeasure> measures)
            : this(name, bpm, new TimeSignature(timeSignature), measures) { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPreset"/> class.</summary>
        /// <param name="name">The name of the preset.</param>
        /// <param name="bpm">The BPM of the preset.</param>
        /// <param name="timeSignature">The time signature of the preset.</param>
        /// <param name="measures">The measures of the preset.</param>
        public GuidelineEditorPreset(string name, double bpm, TimeSignature timeSignature, List<GuidelineEditorPresetMeasure> measures)
        {
            Name = name;
            BPM = bpm;
            TimeSignature = timeSignature;
            Measures = measures;
        }

        /// <summary>Clones this instance of <seealso cref="GuidelineEditorPreset"/>.</summary>
        public GuidelineEditorPreset Clone() => new GuidelineEditorPreset(Name, BPM, TimeSignature, Measures.Clone());

        /// <summary>Parses a <seealso cref="GuidelineEditorPreset"/> from raw data wtih a specified name.</summary>
        /// <param name="name">The name of the preset.</param>
        /// <param name="rawData">The raw data of the preset that will be parsed.</param>
        public static GuidelineEditorPreset Parse(string name, string rawData)
        {
            string[] rawPresetData = rawData.Split('|');
            string[] timeSignature = rawPresetData[1].Split('/');
            var measures = new List<GuidelineEditorPresetMeasure>();
            if (rawPresetData[2].Length > 0)
                // TODO: Investigate why the try-catch is necessary
                try
                {
                    string[] split = rawPresetData[2].Split(';').ExcludeLast(1);
                    foreach (var s in split)
                        measures.Add(GuidelineEditorPresetMeasure.Parse(s));
                }
                catch { }
            return new GuidelineEditorPreset(name, ToDouble(rawPresetData[0]), (ToInt32(timeSignature[0]), ToInt32(timeSignature[1])), measures);
        }

        /// <summary>Gets the string representation of this preset type that will be used for saving the contents in a file.</summary>
        public override string ToString()
        {
            var result = new StringBuilder($"{BPM}|{TimeSignature.Beats}/{TimeSignature.Denominator}|");
            foreach (var m in Measures)
                result.Append($"{m.Colors};");
            return result.RemoveLastIfEndsWith(';').ToString();
        }
    }
}