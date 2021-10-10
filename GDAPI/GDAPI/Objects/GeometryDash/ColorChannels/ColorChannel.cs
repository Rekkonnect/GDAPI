using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.DataStructures;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using static GDAPI.Functions.General.Parsing;

namespace GDAPI.Objects.GeometryDash.ColorChannels
{
    /// <summary>Represents a color channel in a level.</summary>
    public class ColorChannel : IEquatable<ColorChannel>
    {
        private static PropertyInfo[] properties = typeof(ColorChannel).GetProperties();
        private static Dictionary<int, PropertyInfo> mappableProperties = properties.MapCustomAttributesToMembers<PropertyInfo, ColorStringMappableAttribute, int>(ColorStringMappableAttribute.GetKey);

        private byte red, green, blue;
        private short colorChannelID, copiedColorID;
        private float opacity = 1;
        private BitArray8 bools;

        /// <summary>The red color value of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(1)]
        public int Red
        {
            get => red;
            set => red = (byte)value;
        }
        /// <summary>The green color value of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(2)]
        public int Green
        {
            get => green;
            set => green = (byte)value;
        }
        /// <summary>The blue color value of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(3)]
        public int Blue
        {
            get => blue;
            set => blue = (byte)value;
        }
        /// <summary>The copied player color of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(4)]
        public ColorChannelPlayerColor CopiedPlayerColor { get; set; }
        /// <summary>The Blending property of the color channel.</summary>
        [ColorStringMappable(5)]
        public bool Blending
        {
            get => bools[0];
            set => bools[0] = value;
        }
        /// <summary>The Color Channel ID of the color channel.</summary>
        [ColorStringMappable(6)]
        public int ColorChannelID
        {
            get => colorChannelID;
            set => colorChannelID = (short)value;
        }
        /// <summary>The opacity of the color channel.</summary>
        [ColorStringMappable(7)]
        public double Opacity
        {
            get => opacity;
            set => opacity = (float)value;
        }
        /// <summary>Unknown property 8.</summary>
        [ColorStringMappable(8)]
        public bool UnknownProperty8
        {
            get => true;
            set { }
        }
        /// <summary>The Color Channel ID of the copied color channel.</summary>
        [ColorStringMappable(9)]
        public int CopiedColorID
        {
            get => copiedColorID;
            set => copiedColorID = (short)value;
        }
        /// <summary>The HSV adjustment of the copied color channel.</summary>
        [ColorStringMappable(10)]
        public HSVAdjustment CopiedColorHSV { get; set; } = new HSVAdjustment();
        /// <summary>Unknown property 11.</summary>
        [ColorStringMappable(11)]
        public int UnknownProperty11
        {
            get => 255;
            set { }
        }
        /// <summary>Unknown property 12.</summary>
        [ColorStringMappable(12)]
        public int UnknownProperty12
        {
            get => 255;
            set { }
        }
        /// <summary>Unknown property 13.</summary>
        [ColorStringMappable(13)]
        public int UnknownProperty13
        {
            get => 255;
            set { }
        }
        /// <summary>Unknown property 15.</summary>
        [ColorStringMappable(15)]
        public bool UnknownProperty15
        {
            get => true;
            set { }
        }
        /// <summary>The Copy Opacity property of the color channel.</summary>
        [ColorStringMappable(17)]
        public bool CopyOpacity
        {
            get => bools[1];
            set => bools[1] = value;
        }
        /// <summary>Unknown property 18.</summary>
        [ColorStringMappable(18)]
        public bool UnknownProperty18
        {
            get => false;
            set { }
        }

        /// <summary>Gets or sets the color values <seealso cref="Red"/>, <seealso cref="Green"/>, <seealso cref="Blue"/> represented as a <seealso cref="Objects.General.Color"/> (this does not affect <seealso cref="Opacity"/>).</summary>
        public Color Color
        {
            get => new Color(Red, Green, Blue);
            set
            {
                Red = value.IntR;
                Green = value.IntG;
                Blue = value.IntB;
            }
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="ColorChannel"/> class. For private usage only.</summary>
        private ColorChannel() : this(0) { }
        /// <summary>Initializes a new instance of the <seealso cref="ColorChannel"/> class with the default values, them being the white color and the rest being 0.</summary>
        public ColorChannel(int colorChannelID) : this(colorChannelID, 255, 255, 255) { }
        /// <summary>Initializes a new instance of the <seealso cref="ColorChannel"/> class with a specified color, and the default values for the rest.</summary>
        public ColorChannel(int colorChannelID, int red, int green, int blue)
        {
            ColorChannelID = colorChannelID;
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>Resets this <seealso cref="ColorChannel"/>.</summary>
        public void Reset()
        {
            Red = Green = Blue = 255;
            CopiedPlayerColor = ColorChannelPlayerColor.None;
            Blending = false;
            Opacity = 1;
            CopiedColorID = 0;
            CopiedColorHSV.Reset();
            CopyOpacity = false;
        }

        /// <summary>Sets the <seealso cref="ColorChannelID"/> to a new value, while also adjusting the <seealso cref="CopiedColorID"/> property of other <seealso cref="ColorChannel"/>s that depend on this one.</summary>
        /// <param name="value">The new value to set to the <seealso cref="ColorChannelID"/> property.</param>
        /// <param name="potentialDependants">A <seealso cref="List{T}"/> containing <seealso cref="ColorChannel"/>s that may potentially copy this one's color. <seealso cref="ColorChannel"/>s that do not depend on this one are unaffected.</param>
        public void SetColorChannelID(int value, List<ColorChannel> potentialDependants)
        {
            if (value == ColorChannelID)
                return;

            foreach (var p in potentialDependants)
                if (p.CopiedColorID == ColorChannelID)
                    p.CopiedColorID = value;
            ColorChannelID = value;
        }

        public void AssignPropertiesFrom(ColorChannel other)
        {
            ColorChannelID = other.ColorChannelID;
            Red = other.Red;
            Green = other.Green;
            Blue = other.Blue;
            CopiedPlayerColor = other.CopiedPlayerColor;
            Blending = other.Blending;
            CopiedColorID = other.CopiedColorID;
            Opacity = other.Opacity;
            CopiedColorHSV = other.CopiedColorHSV.Clone();
            CopyOpacity = other.CopyOpacity;
        }
        public ColorChannel Clone()
        {
            var result = new ColorChannel();
            result.AssignPropertiesFrom(this);
            return result;
        }

        // TODO: Use reflection
        /// <summary>Parses the color channel string into a <seealso cref="ColorChannel"/> object.</summary>
        /// <param name="colorChannel">The color channel string to parse.</param>
        public static ColorChannel Parse(string colorChannel)
        {
            string[] split = colorChannel.Split('_');
            ColorChannel result = new ColorChannel();
            for (int i = 0; i < split.Length; i += 2)
            {
                int key = ParseInt32(split[i]);
                string value = split[i + 1];
                switch (key)
                {
                    case 1:
                        result.Red = ParseInt32(value);
                        break;
                    case 2:
                        result.Green = ParseInt32(value);
                        break;
                    case 3:
                        result.Blue = ParseInt32(value);
                        break;
                    case 4:
                        result.CopiedPlayerColor = (ColorChannelPlayerColor)ParseInt32(value);
                        break;
                    case 5:
                        result.Blending = value == "1";
                        break;
                    case 6:
                        result.ColorChannelID = ParseInt32(value);
                        break;
                    case 7:
                        result.Opacity = ParseDouble(value);
                        break;
                    case 9:
                        result.CopiedColorID = ParseInt32(value);
                        break;
                    case 10:
                        result.CopiedColorHSV = HSVAdjustment.Parse(value);
                        break;
                    case 17:
                        result.CopyOpacity = value == "1";
                        break;
                    case 8:
                    case 11:
                    case 12:
                    case 13:
                    case 15:
                    case 18:
                        break;
                    default: // We need to know more about that suspicious new thing so we keep a log of it
                        Directory.CreateDirectory("ulscsk");
                        if (!File.Exists($@"ulscsk\{key}.key"))
                            File.WriteAllText($@"ulscsk\{key}.key", key.ToString(CultureInfo.InvariantCulture));
                        break;
                }
            }
            return result;
        }

        public static bool operator ==(ColorChannel left, ColorChannel right)
        {
            bool isLeftNull = ReferenceEquals(left, null);
            bool isRightNull = ReferenceEquals(right, null);
            if (isLeftNull || isRightNull)
                return isLeftNull == isRightNull;
            foreach (var p in mappableProperties)
                if (p.Value.GetValue(left) != p.Value.GetValue(right))
                    return false;
            return true;
        }
        public static bool operator !=(ColorChannel left, ColorChannel right)
        {
            bool isLeftNull = ReferenceEquals(left, null);
            bool isRightNull = ReferenceEquals(right, null);
            if (isLeftNull || isRightNull)
                return isLeftNull != isRightNull;
            foreach (var p in mappableProperties)
                if (p.Value.GetValue(left) == p.Value.GetValue(right))
                    return false;
            return true;
        }

        /// <summary>Returns the string of the <seealso cref="ColorChannel"/>.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var m in mappableProperties)
            {
                var value = m.Value.GetValue(this);
                if (value is bool b)
                    value = b ? 1 : 0;
                result.Append($"{m.Key}_{value}_");
            }
            return result.RemoveLast().ToString();
        }

        public bool Equals(ColorChannel other) => GetHashCode() == other?.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as ColorChannel);
        public override int GetHashCode() => ColorChannelID.GetHashCode();
    }
}
