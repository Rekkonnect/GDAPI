namespace GDAPI.Objects.General
{
    /// <summary>Contains the result of an encryption operation.</summary>
    public class EncryptionResult : BaseCryptoActionResult
    {
        /// <summary>The final encrypted text.</summary>
        public string EncryptedText => ResultingText;

        /// <summary>Initializes a new instance of the <seealso cref="EncryptionResult"/> class.</summary>
        /// <param name="success">Determines whether the operation was successful.</param>
        /// <param name="encryptedText">The resulting encrypted text.</param>
        public EncryptionResult(bool success, string encryptedText)
            : base(success, encryptedText) { }

        public static explicit operator EncryptionResult((bool Success, string ResultingText) tuple) => new EncryptionResult(tuple.Success, tuple.ResultingText);
    }
}
