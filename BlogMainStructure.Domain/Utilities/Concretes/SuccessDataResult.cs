namespace BlogMainStructure.Domain.Utilities.Concretes
{
    // Represents a result containing data with a success status and an optional message.
    public class SuccessDataResult<T> : DataResult<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDataResult{T}"/> class.
        /// </summary>
        public SuccessDataResult() : base(default, true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDataResult{T}"/> class with a message.
        /// </summary>
        /// <param name="message">A message providing additional information about the result.</param>
        public SuccessDataResult(string message) : base(default, true, message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDataResult{T}"/> class with data and a message.
        /// </summary>
        /// <param name="data">The data to include in the result.</param>
        /// <param name="message">A message providing additional information about the result.</param>
        public SuccessDataResult(T data, string message) : base(data, true, message) { }
    }
}