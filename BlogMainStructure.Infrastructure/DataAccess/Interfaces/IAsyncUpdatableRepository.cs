using BlogMainStructure.Domain.Core.BaseEntities;

namespace BlogMainStructure.Infrastructure.DataAccess.Interfaces
{
    // Defines an asynchronous repository with update operations.
    public interface IAsyncUpdatableRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
