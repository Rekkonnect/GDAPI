using GDAPI.Enumerations;

namespace GDAPI.Objects.Music
{
    /// <summary>Represents a rhytmical value.</summary>
    public struct RhythmicalValue
    {
        /// <summary>The musical note value.</summary>
        public MusicalNoteValue NoteValue;
        /// <summary>The dots that multiply the rhythmical value.</summary>
        public int Dots;
        /// <summary>The count of notes that add up the rhythmical value.</summary>
        public int Count;

        /// <summary>The total rhythmical value.</summary>
        public double TotalValue
        {
            get
            {
                double baseValue = 1 / (double)NoteValue;
                double result = baseValue;
                double multiplier = 0.5;
                for (int i = 0; i < Dots; i++, multiplier /= 2)
                    result += multiplier * baseValue;
                return Count * result;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="RhythmicalValue"/> struct.</summary>
        /// <param name="value">The musical note value.</param>
        /// <param name="dots">The dots that multiply the rhythmical value.</param>
        /// <param name="count">The count of notes that add up the rhythmical value.</param>
        public RhythmicalValue(MusicalNoteValue value, int dots, int count)
        {
            NoteValue = value;
            Dots = dots;
            Count = count;
        }
    }
}
