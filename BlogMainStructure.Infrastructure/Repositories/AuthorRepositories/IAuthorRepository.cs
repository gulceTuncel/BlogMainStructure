using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.DataAccess.Interfaces;

namespace BlogMainStructure.Infrastructure.Repositories.AuthorRepositories
{
    // Defines the contract for the repository that manages Author entities.
    // Extends various repository interfaces to support async operations and transaction management.
    public interface IAuthorRepository : IAsyncRepository, IAsyncFindableRepository<Author>, IAsyncInsertableRepository<Author>, IAsyncQueryableRepository<Author>, IAsyncDeletableRepository<Author>, IAsyncUpdatableRepository<Author>, IAsyncTransactionRepository
    {
    }
}
