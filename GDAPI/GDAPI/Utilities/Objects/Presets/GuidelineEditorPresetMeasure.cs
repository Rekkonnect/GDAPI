using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.Presets
{
    /// <summary>Contains information about a measure in a <seealso cref="GuidelineEditorPreset"/>.</summary>
    public class GuidelineEditorPresetMeasure
    {
        /// <summary>The colors of the guideline.</summary>
        public List<decimal> Colors;

        /// <summary>Creates a new empty instance of the <seealso cref="GuidelineEditorPresetMeasure"/> class.</summary>
        public GuidelineEditorPresetMeasure() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetMeasure"/> class.</summary>
        /// <param name="colors">The list of colors to use for this measure.</param>
        public GuidelineEditorPresetMeasure(List<decimal> colors)
        {
            Colors = colors;
        }

        /// <summary>Clones this instance and returns the new instance.</summary>
        public GuidelineEditorPresetMeasure Clone()
        {
            List<decimal> n = new List<decimal>();
            for (int i = 0; i < Colors.Count; i++)
                n.Add(Colors[i]);
            return new GuidelineEditorPresetMeasure(n);
        }
    }
}
