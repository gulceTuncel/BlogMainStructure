using BlogMainStructure.Domain.Core.BaseEntities;
using System.Linq.Expressions;

namespace BlogMainStructure.Infrastructure.DataAccess.Interfaces
{
    // Defines an asynchronous repository with find operations.
    public interface IAsyncFindableRepository<TEntity> where TEntity : BaseEntity
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
    }
}
