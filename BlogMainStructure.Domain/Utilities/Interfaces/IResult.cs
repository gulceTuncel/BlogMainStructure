namespace BlogMainStructure.Domain.Utilities.Interfaces
{
    // Defines the result of an operation, including success status and an optional message.
    public interface IResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
