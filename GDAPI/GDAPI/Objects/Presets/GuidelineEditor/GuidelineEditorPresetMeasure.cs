using System.Collections.Generic;
using System.Text;
using GDAPI.Functions.Extensions;

namespace GDAPI.Objects.Presets.GuidelineEditor
{
    /// <summary>Contains information about a measure in a <seealso cref="GuidelineEditorPresetPattern"/>.</summary>
    public class GuidelineEditorPresetMeasure
    {
        /// <summary>The notes in this measure.</summary>
        public SortedSet<GuidelineEditorPresetNote> Notes;

        /// <summary>Creates a new empty instance of the <seealso cref="GuidelineEditorPresetMeasure"/> class.</summary>
        public GuidelineEditorPresetMeasure() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetMeasure"/> class.</summary>
        /// <param name="notes">The collection of notes to use for this measure.</param>
        public GuidelineEditorPresetMeasure(IEnumerable<GuidelineEditorPresetNote> notes)
        {
            Notes = new SortedSet<GuidelineEditorPresetNote>(notes);
        }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetMeasure"/> class out of a prebuilt <seealso cref="SortedSet{T}"/> without recopying its data. For private usage only.</summary>
        /// <param name="notes">The <seealso cref="SortedSet{T}"/> of notes to use for this measure.</param>
        private GuidelineEditorPresetMeasure(SortedSet<GuidelineEditorPresetNote> notes)
        {
            Notes = notes;
        }

        /// <summary>Clones this instance and returns the new instance.</summary>
        public GuidelineEditorPresetMeasure Clone() => new(Notes);

        /// <summary>Parses a <seealso cref="GuidelineEditorPresetMeasure"/> from raw data.</summary>
        /// <param name="rawData">The raw data of the <seealso cref="GuidelineEditorPresetMeasure"/> that will be parsed.</param>
        public static GuidelineEditorPresetMeasure Parse(string rawData)
        {
            var m = rawData.Split('~');
            var notes = new List<GuidelineEditorPresetNote>(m.Length);
            foreach (var n in m)
                notes.Add(GuidelineEditorPresetNote.Parse(n));
            return new GuidelineEditorPresetMeasure(notes);
        }

        /// <summary>Gets the string representation of this <seealso cref="GuidelineEditorPresetMeasure"/> that will be used in the preset data.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var c in Notes)
                result.Append($"{c};");
            return result.RemoveLastOrNone().ToString();
        }
    }
}
