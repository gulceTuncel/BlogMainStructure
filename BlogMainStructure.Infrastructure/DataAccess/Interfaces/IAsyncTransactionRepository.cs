using Microsoft.EntityFrameworkCore.Storage;

namespace BlogMainStructure.Infrastructure.DataAccess.Interfaces
{
    // Defines an asynchronous repository with transaction operations.
    public interface IAsyncTransactionRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<IExecutionStrategy> CreateExecutionStrategy();
    }
}
