using BlogMainStructure.Domain.Core.BaseEntities;

namespace BlogMainStructure.Infrastructure.DataAccess.Interfaces
{
    // Defines an asynchronous repository with insert operations.
    public interface IAsyncInsertableRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
