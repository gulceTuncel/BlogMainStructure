using BlogMainStructure.Domain.Utilities.Interfaces;

namespace BlogMainStructure.Domain.Utilities.Concretes
{
    // Represents a result of an operation, including success status and a message.
    public class Result : IResult
    {
       
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        public Result()
        {
            IsSuccess = false;
            Message = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with a success status.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with a success status and a message.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        /// <param name="message">A message providing additional information about the result.</param>
        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Message = message;
        }
    }
}