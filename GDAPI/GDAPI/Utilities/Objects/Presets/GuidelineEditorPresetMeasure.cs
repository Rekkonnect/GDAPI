using GDAPI.Utilities.Objects.GeometryDash.General;
using System.Collections.Generic;

namespace GDAPI.Utilities.Objects.Presets
{
    /// <summary>Contains information about a measure in a <seealso cref="GuidelineEditorPreset"/>.</summary>
    public class GuidelineEditorPresetMeasure
    {
        /// <summary>The colors of the guideline.</summary>
        public List<GuidelineColor> Colors;

        /// <summary>Creates a new empty instance of the <seealso cref="GuidelineEditorPresetMeasure"/> class.</summary>
        public GuidelineEditorPresetMeasure() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetMeasure"/> class.</summary>
        /// <param name="colors">The list of colors to use for this measure.</param>
        public GuidelineEditorPresetMeasure(List<GuidelineColor> colors)
        {
            Colors = colors;
        }

        /// <summary>Clones this instance and returns the new instance.</summary>
        public GuidelineEditorPresetMeasure Clone()
        {
            var n = new List<GuidelineColor>();
            for (int i = 0; i < Colors.Count; i++)
                n.Add(Colors[i]);
            return new GuidelineEditorPresetMeasure(n);
        }
    }
}
