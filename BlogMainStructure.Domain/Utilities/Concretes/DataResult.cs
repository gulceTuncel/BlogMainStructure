using BlogMainStructure.Domain.Utilities.Interfaces;

namespace BlogMainStructure.Domain.Utilities.Concretes
{
    // Represents a result containing data, along with success status and an optional message.
    public class DataResult<T> : Result, IDataResult<T> where T : class
    {
        public T? Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataResult{T}"/> class.
        /// </summary>
        /// <param name="data">The data to include in the result.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        public DataResult(T data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataResult{T}"/> class with a message.
        /// </summary>
        /// <param name="data">The data to include in the result.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        /// <param name="message">A message providing additional information about the result.</param>
        public DataResult(T data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }
    }
}