namespace BlogMainStructure.Domain.Utilities.Interfaces
{
    // Defines a result of an operation that includes data, along with success status and an optional message.
    public interface IDataResult<T> : IResult where T : class
    {
        public T? Data { get; }
    }
}
