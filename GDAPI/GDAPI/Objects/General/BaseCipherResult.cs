namespace GDAPI.Objects.General
{
    /// <summary>The base class to support objects containing cipher results.</summary>
    public abstract class BaseCipherResult
    {
        /// <summary>Determines whether the operation was successful.</summary>
        public bool Success { get; }
        /// <summary>The resulting text.</summary>
        protected string ResultingText { get; }

        /// <summary>Initializes a new instance of the <seealso cref="BaseCipherResult"/> class.</summary>
        /// <param name="success">Determines whether the operation was successful.</param>
        /// <param name="resultingText">The resulting text.</param>
        public BaseCipherResult(bool success, string resultingText)
        {
            Success = success;
            ResultingText = resultingText;
        }
    }
}
