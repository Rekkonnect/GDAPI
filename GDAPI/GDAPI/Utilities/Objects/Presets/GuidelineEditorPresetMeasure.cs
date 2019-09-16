using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Objects.GeometryDash.General;
using System.Collections.Generic;
using System.Text;
using static System.Convert;

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

        /// <summary>Parses a <seealso cref="GuidelineEditorPresetMeasure"/> from raw data.</summary>
        /// <param name="rawData">The raw data of the <seealso cref="GuidelineEditorPresetMeasure"/> that will be parsed.</param>
        public static GuidelineEditorPresetMeasure Parse(string rawData)
        {
            var m = rawData.Split(':');
            var colors = new List<GuidelineColor>(m.Length);
            foreach (var c in m)
                colors.Add(ToSingle(c));
            return new GuidelineEditorPresetMeasure(colors);
        }

        /// <summary>Gets the string representation of this <seealso cref="GuidelineEditorPresetMeasure"/> that will be used in the preset data.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var c in Colors)
                result.Append($"{c}:");
            return result.RemoveLastOrNone().ToString();
        }
    }
}
