using System;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.Music;

namespace GDAPI.Objects.Presets.GuidelineEditor
{
    /// <summary>Represents a note in a <seealso cref="GuidelineEditorPresetMeasure"/>.</summary>
    public struct GuidelineEditorPresetNote : IComparable<GuidelineEditorPresetNote>
    {
        /// <summary>The position of this note.</summary>
        public MeasuredTimePosition Position;
        /// <summary>The guideline color to use for this note.</summary>
        public GuidelineColor Color;

        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetNote"/> struct.</summary>
        /// <param name="position">The position of this note.</param>
        /// <param name="color">The guideline color to use for this note.</param>
        public GuidelineEditorPresetNote(MeasuredTimePosition position, GuidelineColor color)
        {
            Position = position;
            Color = color;
        }

        /// <summary>Parses a <seealso cref="GuidelineEditorPresetNote"/> from raw data.</summary>
        /// <param name="rawData">The raw data of the <seealso cref="GuidelineEditorPresetNote"/> that will be parsed.</param>
        public static GuidelineEditorPresetNote Parse(string rawData)
        {
            var m = rawData.Split(':');
            return new GuidelineEditorPresetNote(MeasuredTimePosition.ParseAsBeatWithFraction(m[0]), float.Parse(m[1]));
        }

        /// <summary>Compares this <seealso cref="GuidelineEditorPresetNote"/> with another and returns the comparison result based on their positions' <seealso cref="MeasuredTimePosition.BeatWithFraction"/>.</summary>
        /// <param name="other">The other <seealso cref="GuidelineEditorPresetNote"/> to compare this with.</param>
        public int CompareTo(GuidelineEditorPresetNote other) => Position.BeatWithFraction.CompareTo(other.Position.BeatWithFraction);

        /// <summary>Gets the string representation of this <seealso cref="GuidelineEditorPresetNote"/> that will be used in the preset data.</summary>
        public override string ToString() => $"{Position.BeatWithFraction}:{Color}";
    }
}
