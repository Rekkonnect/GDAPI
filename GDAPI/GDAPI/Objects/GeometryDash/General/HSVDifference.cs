using System;
using System.Globalization;
using static GDAPI.Functions.General.Parsing;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Represents the HSV adjustment in an object's color or a trigger's copied color HSV adjustment.</summary>
    public class HSVAdjustment
    {
        public const string DefaultHSVString = "0a1a1a0a0";

        private H h;
        private SV s, v;
        private SVAdjustmentMode saturationMode;
        private SVAdjustmentMode brightnessMode;
        
        /// <summary>The hue of the HSV adjustment.</summary>
        public double Hue
        {
            get => h.Value;
            set => h.Value = (short)value;
        }
        /// <summary>The saturation of the HSV adjustment.</summary>
        public double Saturation
        {
            get => s.Value;
            set => s.Value = (float)value;
        }
        /// <summary>The brightness of the HSV adjustment.</summary>
        public double Brightness
        {
            get => v.Value;
            set => v.Value = (float)value;
        }
        /// <summary>The adjustment mode of the saturation of the HSV adjustment.</summary>
        public SVAdjustmentMode SaturationMode
        {
            get => saturationMode;
            set
            {
                s = SV.GetSVFromMode(value, s.Value);
                saturationMode = value;
            }
        }
        /// <summary>The adjustment mode of the brightness of the HSV adjustment.</summary>
        public SVAdjustmentMode BrightnessMode
        {
            get => brightnessMode;
            set
            {
                v = SV.GetSVFromMode(value, v.Value);
                brightnessMode = value;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="HSVAdjustment"/> class.</summary>
        public HSVAdjustment() : this(0, 1, 1, default, default) { }
        /// <summary>Initializes a new instance of the <seealso cref="HSVAdjustment"/> class.</summary>
        /// <param name="hue">The hue of the HSV adjustment.</param>
        /// <param name="saturation">The saturation of the HSV adjustment.</param>
        /// <param name="brightness">The brightness of the HSV adjustment.</param>
        /// <param name="sMode">The adjustment mode of the saturation of the HSV adjustment.</param>
        /// <param name="bMode">The adjustment mode of the brightness of the HSV adjustment.</param>
        public HSVAdjustment(double hue, double saturation, double brightness, SVAdjustmentMode sMode, SVAdjustmentMode bMode)
        {
            h = new H((short)hue);
            s = SV.GetSVFromMode(sMode, (float)saturation);
            v = SV.GetSVFromMode(bMode, (float)brightness);
            saturationMode = sMode;
            brightnessMode = bMode;
        }

        public static bool operator ==(HSVAdjustment left, HSVAdjustment right)
        {
            return left.h == right.h
                && left.s == right.s
                && left.v == right.v
                && left.saturationMode == right.saturationMode
                && left.brightnessMode == right.brightnessMode;
        }
        public static bool operator !=(HSVAdjustment left, HSVAdjustment right)
        {
            return left.h != right.h
                || left.s != right.s
                || left.v != right.v
                || left.saturationMode != right.saturationMode
                || left.brightnessMode != right.brightnessMode;
        }

        /// <summary>Resets this <seealso cref="HSVAdjustment"/>.</summary>
        public void Reset()
        {
            Hue = 0;
            Saturation = Brightness = 1;
            SaturationMode = BrightnessMode = SVAdjustmentMode.Multiplicative;
        }

        /// <summary>Clones this instance and returns a new instance with the same value.</summary>
        public HSVAdjustment Clone()
        {
            var result = new HSVAdjustment();
            result.h = h.Clone();
            result.s = s.Clone();
            result.v = v.Clone();
            result.saturationMode = saturationMode;
            result.brightnessMode = brightnessMode;
            return result;
        }

        /// <summary>Parses the HSV adjustment string into an <seealso cref="HSVAdjustment"/> object.</summary>
        /// <param name="adjustment">The string to parse.</param>
        public static HSVAdjustment Parse(string adjustment)
        {
            string[] split = adjustment.Split('a');
            return new HSVAdjustment(ParseDouble(split[0]), ParseDouble(split[1]), ParseDouble(split[2]), (SVAdjustmentMode)ParseInt32(split[3]), (SVAdjustmentMode)ParseInt32(split[4]));
        }

        public override string ToString() => $"{Hue.ToString(CultureInfo.InvariantCulture)}a{Saturation.ToString(CultureInfo.InvariantCulture)}a{Brightness.ToString(CultureInfo.InvariantCulture)}a{(int)SaturationMode}a{(int)BrightnessMode}";

        // The gay code below is unfortunately necessary
        #region Bullshit
        private abstract class BoundedValue<T>
        {
            private T v;

            /// <summary>The minimum value of the <seealso cref="BoundedValue{T}"/>.</summary>
            public abstract T Min { get; }
            /// <summary>The maximum value of the <seealso cref="BoundedValue{T}"/>.</summary>
            public abstract T Max { get; }
            /// <summary>The value of the <seealso cref="BoundedValue{T}"/>.</summary>
            public T Value
            {
                get => v;
                set => v = Clamp(value);
            }

            /// <summary>Initializes a new instance of the <seealso cref="BoundedValue{T}"/> class.</summary>
            /// <param name="value">The value of the <seealso cref="BoundedValue{T}"/>.</param>
            /// <param name="min">The minimum value of the <seealso cref="BoundedValue{T}"/>.</param>
            /// <param name="max">The maximum value of the <seealso cref="BoundedValue{T}"/>.</param>
            public BoundedValue(T value)
            {
                Value = value;
            }

            public static implicit operator T(BoundedValue<T> b) => b.Value;

            /// <summary>Clamps the value between the two boundaries and returns the clamped result.</summary>
            /// <param name="value">The value to clamp.</param>
            protected abstract T Clamp(T value);

            /// <summary>Detetmines whether this instance's value equals a specific value.</summary>
            /// <param name="value">The value to compare this instance's value with.</param>
            protected abstract bool EqualsValue(T value);

            public static bool operator ==(BoundedValue<T> left, BoundedValue<T> right) => left.EqualsValue(right.Value);
            public static bool operator !=(BoundedValue<T> left, BoundedValue<T> right) => !left.EqualsValue(right.Value);
        }
        private abstract class BoundedFloat : BoundedValue<float>
        {
            public BoundedFloat(float value) : base(value) { }

            protected override float Clamp(float value)
            {
                if (value < Min)
                    value = Min;
                if (value > Max)
                    value = Max;
                return value;
            }

            protected override bool EqualsValue(float value) => Value == value;
        }
        private abstract class BoundedShort : BoundedValue<short>
        {
            public BoundedShort(short value) : base(value) { }

            protected override short Clamp(short value)
            {
                if (value < Min)
                    value = Min;
                if (value > Max)
                    value = Max;
                return value;
            }

            protected override bool EqualsValue(short value) => Value == value;
        }
        private class H : BoundedShort
        {
            public override short Min => -180;
            public override short Max => 180;

            public H(short value) : base(value) { }

            /// <summary>Clones this instance and returns a new instance with the same value.</summary>
            public H Clone() => new(Value);
        }
        private abstract class SV : BoundedFloat
        {
            protected abstract float BoundaryOffset { get; }

            public override float Min => 0 + BoundaryOffset;
            public override float Max => 2 + BoundaryOffset;

            public SV(float value) : base(value) { }

            public static SV GetSVFromMode(SVAdjustmentMode mode, float value)
            {
                switch (mode)
                {
                    case SVAdjustmentMode.Multiplicative:
                        return new UncheckedSV(value);
                    case SVAdjustmentMode.Additive:
                        return new CheckedSV(value);
                    default:
                        throw new Exception("The SV adjustment mode seriously cannot be anything other than those two, what have you done?");
                }
            }

            /// <summary>Clones this instance and returns a new instance with the same value.</summary>
            public abstract SV Clone();
        }
        private class UncheckedSV : SV
        {
            protected override float BoundaryOffset => 0;

            public UncheckedSV(float value) : base(value) { }

            /// <summary>Clones this instance and returns a new instance with the same value.</summary>
            public override SV Clone() => new UncheckedSV(Value);
        }
        private class CheckedSV : SV
        {
            protected override float BoundaryOffset => -1;

            public CheckedSV(float value) : base(value) { }

            /// <summary>Clones this instance and returns a new instance with the same value.</summary>
            public override SV Clone() => new CheckedSV(Value);
        }
        #endregion
    }

    /// <summary>Represents the mode of the HSV adjustment for the saturation and the brightness properties.</summary>
    public enum SVAdjustmentMode : byte
    {
        Multiplicative = 0,
        Additive = 1,
    }
}
