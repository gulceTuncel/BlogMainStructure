using BlogMainStructure.Domain.Core.BaseEntities;
using System.Linq.Expressions;

namespace BlogMainStructure.Infrastructure.DataAccess.Interfaces
{
    // Defines an asynchronous repository with querying capabilities.
    public interface IAsyncQueryableRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true); //koşulsuz hepsini getiriyor
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true); //koşula uyan hepsini getiriyor
    }
}