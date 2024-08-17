namespace BlogMainStructure.Domain.Utilities.Concretes
{
    // Represents a successful result with an optional message.
    public class SuccessResult : Result
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult"/> class.
        /// </summary>
        public SuccessResult() : base(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult"/> class with a message.
        /// </summary>
        /// <param name="message">A message providing additional information about the result.</param>
        public SuccessResult(string message) : base(true, message) { }
    }
}
