namespace BlogMainStructure.Infrastructure.DataAccess.Interfaces
{
    // Defines an asynchronous repository with basic save operations.
    public interface IAsyncRepository
    {
        Task<int> SaveChangeAsync();
    }
}
