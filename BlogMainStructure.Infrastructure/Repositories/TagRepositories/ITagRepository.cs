using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.DataAccess.Interfaces;

namespace BlogMainStructure.Infrastructure.Repositories.TagRepositories
{
    // Defines the contract for the repository that manages Tag entities.
    // Extends various repository interfaces to support async operations.
    public interface ITagRepository : IRepository, IAsyncInsertableRepository<Tag>, IAsyncFindableRepository<Tag>, IAsyncOrderableRepository<Tag>, IAsyncQueryableRepository<Tag>, IAsyncRepository, IAsyncUpdatableRepository<Tag>, IAsyncDeletableRepository<Tag>
    {
    }
}
