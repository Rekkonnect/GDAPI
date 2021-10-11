using System;

namespace GDAPI.Objects.General
{
    /// <summary>Represents an ARGB color.</summary>
    public struct Color
    {
        public static readonly Color White = new(1f);
        public static readonly Color Black = new(0, 0, 0);
        public static readonly Color BlackTransparent = new(0);
        public static readonly Color Zero = new(0);

        private ColorValue r, g, b, a;

        /// <summary>The red value of the color.</summary>
        public float R
        {
            get => r;
            set => r = value;
        }
        /// <summary>The green value of the color.</summary>
        public float G
        {
            get => g;
            set => g = value;
        }
        /// <summary>The blue value of the color.</summary>
        public float B
        {
            get => b;
            set => b = value;
        }
        /// <summary>The alpha value of the color.</summary>
        public float A
        {
            get => a;
            set => a = value;
        }
        /// <summary>The 8-bit red value of the color.</summary>
        public int IntR
        {
            get => GetIntValue(r);
            set => r = GetFloatValue(value);
        }
        /// <summary>The 8-bit green value of the color.</summary>
        public int IntG
        {
            get => GetIntValue(g);
            set => g = GetFloatValue(value);
        }
        /// <summary>The 8-bit blue value of the color.</summary>
        public int IntB
        {
            get => GetIntValue(b);
            set => b = GetFloatValue(value);
        }
        /// <summary>The 8-bit alpha value of the color.</summary>
        public int IntA
        {
            get => GetIntValue(a);
            set => a = GetFloatValue(value);
        }

        /// <summary>Initializes a new instance of the <seealso cref="Color"/> struct.</summary>
        /// <param name="all">The value for all the color values (R, G, B, A).</param>
        public Color(float all) : this(all, all, all, all) { }
        /// <summary>Initializes a new instance of the <seealso cref="Color"/> struct.</summary>
        /// <param name="red">The red value of the color.</param>
        /// <param name="green">The green value of the color.</param>
        /// <param name="blue">The blue value of the color.</param>
        public Color(float red, float green, float blue) : this(red, green, blue, 1) { }
        /// <summary>Initializes a new instance of the <seealso cref="Color"/> struct.</summary>
        /// <param name="red">The red value of the color.</param>
        /// <param name="green">The green value of the color.</param>
        /// <param name="blue">The blue value of the color.</param>
        /// <param name="alpha">The alpha value of the color.</param>
        public Color(float red, float green, float blue, float alpha)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }
        /// <summary>Initializes a new instance of the <seealso cref="Color"/> struct.</summary>
        /// <param name="all">The 8-bit value for all the color values (R, G, B, A).</param>
        public Color(int all) : this(all, all, all, all) { }
        /// <summary>Initializes a new instance of the <seealso cref="Color"/> struct.</summary>
        /// <param name="red">The 8-bit red value of the color.</param>
        /// <param name="green">The 8-bit green value of the color.</param>
        /// <param name="blue">The 8-bit blue value of the color.</param>
        public Color(int red, int green, int blue) : this(GetFloatValue(red), GetFloatValue(green), GetFloatValue(blue)) { }
        /// <summary>Initializes a new instance of the <seealso cref="Color"/> struct.</summary>
        /// <param name="red">The 8-bit red value of the color.</param>
        /// <param name="green">The 8-bit green value of the color.</param>
        /// <param name="blue">The 8-bit blue value of the color.</param>
        /// <param name="alpha">The 8-bit alpha value of the color.</param>
        public Color(int red, int green, int blue, int alpha) : this(GetFloatValue(red), GetFloatValue(green), GetFloatValue(blue), GetFloatValue(alpha)) { }

        public static bool operator ==(Color left, Color right) => left.r == right.r && left.g == right.g && left.b == right.b && left.a == right.a;
        public static bool operator !=(Color left, Color right) => left.r != right.r && left.g != right.g && left.b != right.b && left.a != right.a;

        private static int GetIntValue(float f) => (int)(f * 255 + 0.5f);
        private static float GetFloatValue(int i) => i / 255f;

        private struct ColorValue
        {
            private float v;

            public float Value
            {
                get => v;
                set
                {
                    if (value < 0 || value > 1)
                        throw new ArgumentException("The color value cannot be outside the range [0, 1].");
                    v = value;
                }
            }

            public ColorValue(float value)
            {
                v = value;
                Value = v;
            }

            public static implicit operator float(ColorValue v) => v.v;
            public static implicit operator ColorValue(float f) => new(f);

            public static bool operator ==(ColorValue left, ColorValue right) => left.v == right.v;
            public static bool operator !=(ColorValue left, ColorValue right) => left.v != right.v;
        }
    }
}
