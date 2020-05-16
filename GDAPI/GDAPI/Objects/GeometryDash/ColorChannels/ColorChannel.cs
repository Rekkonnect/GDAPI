using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using System.Collections.Generic;
using System.IO;
using static System.Convert;

namespace GDAPI.Objects.GeometryDash.ColorChannels
{
    /// <summary>Represents a color channel in a level.</summary>
    public class ColorChannel
    {
        /// <summary>The red color value of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(1)]
        public int Red { get; set; }
        /// <summary>The green color value of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(2)]
        public int Green { get; set; }
        /// <summary>The blue color value of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(3)]
        public int Blue { get; set; }
        /// <summary>The copied player color of the <seealso cref="ColorChannel"/>.</summary>
        [ColorStringMappable(4)]
        public ColorChannelPlayerColor CopiedPlayerColor { get; set; }
        /// <summary>The Blending property of the color channel.</summary>
        [ColorStringMappable(5)]
        public bool Blending { get; set; }
        /// <summary>The Color Channel ID of the color channel.</summary>
        [ColorStringMappable(6)]
        public int ColorChannelID { get; set; }
        /// <summary>The opacity of the color channel.</summary>
        [ColorStringMappable(7)]
        public double Opacity { get; set; }
        /// <summary>The Color Channel ID of the copied color channel.</summary>
        [ColorStringMappable(9)]
        public int CopiedColorID { get; set; }
        /// <summary>The HSV adjustment of the copied color channel.</summary>
        [ColorStringMappable(10)]
        public HSVAdjustment CopiedColorHSV { get; set; } = new HSVAdjustment();
        /// <summary>The Copy Opacity property of the color channel.</summary>
        [ColorStringMappable(17)]
        public bool CopyOpacity { get; set; }

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

        public ColorChannel Clone()
        {
            var result = new ColorChannel(ColorChannelID, Red, Green, Blue);
            result.CopiedPlayerColor = result.CopiedPlayerColor;
            result.Blending = Blending;
            result.CopiedColorID = result.CopiedColorID;
            result.Opacity = Opacity;
            result.CopiedColorHSV = CopiedColorHSV.Clone();
            result.CopyOpacity = CopyOpacity;
            return result;
        }
        
        /// <summary>Parses the color channel string into a <seealso cref="ColorChannel"/> object.</summary>
        /// <param name="colorChannel">The color channel string to parse.</param>
        public static ColorChannel Parse(string colorChannel)
        {
            string[] split = colorChannel.Split('_');
            ColorChannel result = new ColorChannel();
            for (int i = 0; i < split.Length; i += 2)
            {
                int key = ToInt32(split[i]);
                string value = split[i + 1];
                switch (key)
                {
                    case 1:
                        result.Red = ToInt32(value);
                        break;
                    case 2:
                        result.Green = ToInt32(value);
                        break;
                    case 3:
                        result.Blue = ToInt32(value);
                        break;
                    case 4:
                        result.CopiedPlayerColor = (ColorChannelPlayerColor)ToInt32(value);
                        break;
                    case 5:
                        result.Blending = value == "1";
                        break;
                    case 6:
                        result.ColorChannelID = ToInt32(value);
                        break;
                    case 7:
                        result.Opacity = ToDouble(value);
                        break;
                    case 9:
                        result.CopiedColorID = ToInt32(value);
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
                            File.WriteAllText($@"ulscsk\{key}.key", key.ToString());
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
            foreach (var p in typeof(ColorChannel).GetProperties())
                if (p.GetValue(left) != p.GetValue(right))
                    return false;
            return true;
        }
        public static bool operator !=(ColorChannel left, ColorChannel right)
        {
            bool isLeftNull = ReferenceEquals(left, null);
            bool isRightNull = ReferenceEquals(right, null);
            if (isLeftNull || isRightNull)
                return isLeftNull != isRightNull;
            foreach (var p in typeof(ColorChannel).GetProperties())
                if (p.GetValue(left) == p.GetValue(right))
                    return false;
            return true;
        }

        // IMPORTANT: This may need to be changed as more information about the color channel string is discovered (especially for property IDs 8, 11, 12, 13, 15, 18 which are currently hardcoded because of that)
        /// <summary>Returns the string of the <seealso cref="ColorChannel"/>.</summary>
        public override string ToString() => $"1_{Red}_2_{Green}_3_{Blue}_4_{(int)CopiedPlayerColor}_5_{(Blending ? 1 : 0)}_6_{ColorChannelID}_7_{Opacity}_8_1_9_{CopiedColorID}_10_{CopiedColorHSV}_11_255_12_255_13_255_15_1_17_{(CopyOpacity ? 1 : 0)}_18_0";
    }
}
