namespace GDAPI.Objects.General
{
    /// <summary>Contains the result of a decryption operation.</summary>
    public class DecryptionResult : BaseCipherResult
    {
        /// <summary>The final decrypted text.</summary>
        public string DecryptedText => ResultingText;

        /// <summary>Initializes a new instance of the <seealso cref="DecryptionResult"/> class.</summary>
        /// <param name="success">Determines whether the operation was successful.</param>
        /// <param name="decryptedText">The resulting decrypted text.</param>
        public DecryptionResult(bool success, string decryptedText)
            : base(success, decryptedText) { }

        public static explicit operator DecryptionResult((bool Success, string ResultingText) tuple) => new(tuple.Success, tuple.ResultingText);
    }
}
