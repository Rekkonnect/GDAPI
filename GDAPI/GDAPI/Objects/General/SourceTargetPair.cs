using System;

namespace GDAPI.Objects.General
{
    /// <summary>Represents a source-target pair, indicating a difference that an operation has created.</summary>
    public struct SourceTargetPair : IEquatable<SourceTargetPair>
    {
        /// <summary>The source value.</summary>
        public int Source { get; set; }
        /// <summary>The target value.</summary>
        public int Target { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="SourceTargetPair"/> struct.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="target">The target value.</param>
        public SourceTargetPair(int source, int target)
        {
            Source = source;
            Target = target;
        }

        /// <summary>Adds a value to both <seealso cref="Source"/> and <seealso cref="Target"/>.</summary>
        /// <param name="value">The value to add to both.</param>
        public void Add(int value)
        {
            Source += value;
            Target += value;
        }
        /// <summary>Subtracts a value from both <seealso cref="Source"/> and <seealso cref="Target"/>.</summary>
        /// <param name="value">The value to subtract from both.</param>
        public void Subtract(int value) => Add(-value);

        public static bool operator ==(SourceTargetPair left, SourceTargetPair right) => left.Equals(right);
        public static bool operator !=(SourceTargetPair left, SourceTargetPair right) => !(left == right);

        public bool Equals(SourceTargetPair other) => Source == other.Source && Target == other.Target;
        public override bool Equals(object obj) => Equals((SourceTargetPair)obj);
        public override int GetHashCode() => HashCode.Combine(Source, Target);
        /// <summary>Returns a string representation of this <seealso cref="SourceTargetPair"/> instance.</summary>
        public override string ToString() => $"{Source} > {Target}";
    }
}
