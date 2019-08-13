namespace GDAPI.Utilities.Objects.General
{
    /// <summary>Represents an array of 16 bits compactly stored in a <seealso cref="ushort"/>.</summary>
    public struct BitArray16
    {
        // The LSB has index 0 and the MSB has index 15
        private ushort bits;
        
        /// <summary>Initializes a new instance of the <seealso cref="BitArray16"/> struct.</summary>
        /// <param name="defaultValue">The default value to set to all the bits.</param>
        public BitArray16(bool defaultValue = false)
        {
            bits = (ushort)(defaultValue ? 0b1111_1111_1111_1111 : 0); // Hardcode for perfomance
        }
        
        /// <summary>Gets a bit of the <seealso cref="BitArray16"/> at the specified index as a <seealso cref="bool"/>.</summary>
        /// <param name="index">The index of the bit to get.</param>
        public bool GetBoolBit(int index) => GetBool(GetBit(index));
        /// <summary>Sets a bit of the <seealso cref="BitArray16"/> at the specified index as a <seealso cref="bool"/>.</summary>
        /// <param name="index">The index of the bit to set.</param>
        /// <param name="b">The bit to set at the specified index as a <seealso cref="bool"/>.</param>
        public void SetBoolBit(int index, bool b) => SetBit(index, GetByte(b));
        
        // All those casts are retarded
        /// <summary>Gets a bit of the <seealso cref="BitArray16"/> at the specified index.</summary>
        /// <param name="index">The index of the bit to get.</param>
        public ushort GetBit(int index) => (ushort)((bits & (ushort)(1 << index)) >> index);
        /// <summary>Sets a bit of the <seealso cref="BitArray16"/> at the specified index.</summary>
        /// <param name="index">The index of the bit to set.</param>
        /// <param name="b">The bit to set at the specified index.</param>
        public void SetBit(int index, byte b) => bits = (ushort)((ushort)(bits & (ushort)~(1 << index)) | (ushort)(b << index));
        
        /// <summary>Gets or sets a bit of the <seealso cref="BitArray16"/> at the specified index.</summary>
        /// <param name="index">The index of the bit to get or set.</param>
        public bool this[int index]
        {
            get => GetBoolBit(index);
            set => SetBoolBit(index, value);
        }

        public static bool operator ==(BitArray16 left, BitArray16 right) => left.bits == right.bits;
        public static bool operator !=(BitArray16 left, BitArray16 right) => left.bits != right.bits;

        // Methods are private to avoid handling exceptions
        private static bool GetBool(ushort b) => b == 1;
        private static byte GetByte(bool b) => b ? (byte)1 : (byte)0;
    }
}