using System.Globalization;

namespace BlogMainStructure.Domain.Utilities.Concretes
{
    // Represents an error result with a message.
    public class ErrorResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class.
        /// </summary>
        public ErrorResult() : base(false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class with a message.
        /// </summary>
        /// <param name="message">A message providing additional information about the error.</param>
        public ErrorResult(string message) : base(false, message) { }
    }
}