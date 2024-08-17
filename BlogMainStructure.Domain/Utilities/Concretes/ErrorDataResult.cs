namespace BlogMainStructure.Domain.Utilities.Concretes
{
    // Represents a result containing data with an error status and an optional message.
    public class ErrorDataResult<T> : DataResult<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDataResult{T}"/> class.
        /// </summary>
        public ErrorDataResult() : base(default, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDataResult{T}"/> class with a message.
        /// </summary>
        /// <param name="message">A message providing additional information about the error.</param>
        public ErrorDataResult(string message) : base(default, false, message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDataResult{T}"/> class with data and a message.
        /// </summary>
        /// <param name="data">The data to include in the result.</param>
        /// <param name="message">A message providing additional information about the error.</param>
        public ErrorDataResult(T data, string message) : base(data, false, message) { }
    }
}