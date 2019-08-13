using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.General
{
    /// <summary>Represents a time signature.</summary>
    public struct TimeSignature
    {
        private int b, d;

        /// <summary>The beats of the time signature.</summary>
        public int Beats
        {
            get => b;
            set
            {
                if (value < 1)
                    throw new InvalidOperationException("Cannot set the beats of the time signature to a non-positive integer.");
                b = value;
            }
        }
        /// <summary>The denominator of the time signature.</summary>
        public int Denominator
        {
            get => d;
            set
            {
                if (value < 1)
                    throw new InvalidOperationException("Cannot set the denominator of the time signature to a non-positive integer.");
                if (!IsPowerOfTwo(d))
                    throw new InvalidOperationException("Cannot set the denominator of the time signature to anything but a power of two.");
                d = value;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="TimeSignature"/> class.</summary>
        /// <param name="beats">The beats of the time signature.</param>
        /// <param name="denominator">The denominator of the time signature.</param>
        public TimeSignature(int beats, int denominator)
        {
            b = beats;
            d = denominator;
            Beats = beats;
            Denominator = denominator;
        }
        /// <summary>Initializes a new instance of the <seealso cref="TimeSignature"/> class.</summary>
        /// <param name="beats">The beats of the time signature.</param>
        public TimeSignature((int b, int d) timeSignature) : this(timeSignature.b, timeSignature.d) { }

        private bool IsPowerOfTwo(int value)
        {
            while (value > 1)
            {
                if (value % 2 == 1)
                    return false;
                value >>= 1;
            }
            return true;
        }
    }
}
