namespace GDAPI.Objects.General
{
    /// <summary>The base class to support objects containing crypto action results.</summary>
    public class BaseCryptoActionResult
    {
        /// <summary>Determines whether the operation was successful.</summary>
        public bool Success { get; }
        /// <summary>The resulting text.</summary>
        protected string ResultingText { get; }

        /// <summary>Initializes a new instance of the <seealso cref="BaseCryptoActionResult"/> class.</summary>
        /// <param name="success">Determines whether the operation was successful.</param>
        /// <param name="resultingText">The resulting text.</param>
        public BaseCryptoActionResult(bool success, string resultingText)
        {
            Success = success;
            ResultingText = resultingText;
        }
    }
}
