using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GDAPI.Utilities.Information.GeometryDash.Speeds;

namespace GDAPI.Utilities.Objects.GeometryDash
{
    /// <summary>Represents a sorted speed segment collection.</summary>
    public class SpeedSegmentCollection : SortedSet<SpeedSegment>
    {
        /// <summary>Initializes a new instance of the <seealso cref="SpeedSegmentCollection"/> class.</summary>
        public SpeedSegmentCollection() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpeedSegmentCollection"/> class.</summary>
        /// <param name="segments">The speed segments to create this <seealso cref="SpeedSegmentCollection"/> from.</param>
        public SpeedSegmentCollection(SortedSet<SpeedSegment> segments) : base(segments) { }

        /// <summary>Converts the provided X position into time.</summary>
        /// <param name="x">The X position to convert into time.</param>
        public double ConvertXToTime(double x)
        {
            double time = 0;
            int i = 1;
            for (; i < Count && this[i].X < x; i++)
                time += (this[i].X - this[i - 1].X) / GetSpeed(this[i - 1].Speed);
            int final = Math.Min(i, Count - 1);
            return time + (x - this[final].X) / GetSpeed(this[final].Speed);
        }
        /// <summary>Converts the provided time into X position.</summary>
        /// <param name="time">The time to convert into X position.</param>
        public double ConvertTimeToX(double time)
        {
            double t = 0;
            int i = 1;
            for (; i < Count && t < time; i++)
                t += (this[i].X - this[i - 1].X) / GetSpeed(this[i - 1].Speed);
            int final = Math.Min(i, Count) - 1;
            return this[final].X + (t - time) * GetSpeed(this[final].Speed);
        }

        /// <summary>Converts the provided X positions into times.</summary>
        /// <param name="x">The X positions to convert into times.</param>
        public List<double> ConvertXToTime(List<double> x)
        {
            x.Sort();
            double time = 0;
            int i = 1;
            var result = new List<double>();
            foreach (var k in x)
            {
                for (; i < Count && this[i].X < k; i++)
                    time += (this[i].X - this[i - 1].X) / GetSpeed(this[i - 1].Speed);
                int final = Math.Min(i, Count - 1);
                result.Add(time + (k - this[final].X) / GetSpeed(this[final].Speed));
            }
            return result;
        }
        /// <summary>Converts the provided times into X positions.</summary>
        /// <param name="times">The times to convert into X positions.</param>
        public List<double> ConvertTimeToX(List<double> times)
        {
            times.Sort();
            double t = 0;
            int i = 1;
            var result = new List<double>();
            foreach (var k in times)
            {
                for (; i < Count && t < k; i++)
                    t += (this[i].X - this[i - 1].X) / GetSpeed(this[i - 1].Speed);
                int final = Math.Min(i, Count) - 1;
                result.Add(this[final].X + (t - k) * GetSpeed(this[final].Speed));
            }
            return result;
        }

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <param name="index">The index of the element to get or set.</param>
        public SpeedSegment this[int index]
        {
            get => this.ElementAt(index);
            set
            {
                // Why does it have to be like that?
                Remove(this.ElementAt(index));
                Add(value);
            }
        }
    }
}
