using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Objects.General;
using System.Collections.Generic;

namespace GDAPI.Utilities.Objects.Presets
{
    /// <summary>Represents a preset for the guideline editor.</summary>
    public class GuidelineEditorPreset
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
    }
}