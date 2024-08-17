using BlogMainStructure.Domain.Core.BaseEntities;
using BlogMainStructure.Domain.Enums;
using BlogMainStructure.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace BlogMainStructure.Infrastructure.DataAccess.EntityFramework
{
    // Provides a base repository implementation for entity operations using Entity Framework.
    public class EFBaseRepository<TEntity> : IRepository, IAsyncRepository, IAsyncInsertableRepository<TEntity>, IAsyncUpdatableRepository<TEntity>, IAsyncDeletableRepository<TEntity>, IAsyncFindableRepository<TEntity>, IAsyncQueryableRepository<TEntity>, IAsyncOrderableRepository<TEntity>, IAsyncTransactionRepository where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _table;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFBaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public EFBaseRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        /// <summary>
        /// Asynchronously adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>The added entity.</returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                var entry = await _table.AddAsync(entity);
                return entry.Entity;
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during AddAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously adds a range of entities to the database.
        /// </summary>
        /// <param name="entities">The entities to be added.</param>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await _table.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during AddRangeAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously checks if any entity matches the specified condition.
        /// </summary>
        /// <param name="expression">The condition to check.</param>
        /// <returns>True if any entity matches the condition; otherwise, false.</returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                return expression is null ? await GetAllActives().AnyAsync() : await GetAllActives().AnyAsync(expression);
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during AnyAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Begins a new transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous transaction operation.</returns>
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return _context.Database.BeginTransactionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during BeginTransactionAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Creates a new execution strategy.
        /// </summary>
        /// <returns>A task representing the asynchronous execution strategy creation.</returns>
        public Task<IExecutionStrategy> CreateExecutionStrategy()
        {
            try
            {
                return Task.FromResult(_context.Database.CreateExecutionStrategy());
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during CreateExecutionStrategy: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                _table.Remove(entity);
                await SaveChangeAsync();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during DeleteAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a range of entities from the database.
        /// </summary>
        /// <param name="entities">The entities to be deleted.</param>
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                _table.RemoveRange(entities);
                await SaveChangeAsync();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during DeleteRangeAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all entities from the database.
        /// </summary>
        /// <param name="tracking">Specifies whether to track changes.</param>
        /// <returns>An enumerable of all entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
        {
            try
            {
                return await GetAllActives(tracking).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during GetAllAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all entities that match the specified condition.
        /// </summary>
        /// <param name="expression">The condition to filter entities.</param>
        /// <param name="tracking">Specifies whether to track changes.</param>
        /// <returns>An enumerable of matching entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            try
            {
                return await GetAllActives(tracking).Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during GetAllAsync with condition: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all entities ordered by the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to order by.</typeparam>
        /// <param name="orderBy">The key selector to order by.</param>
        /// <param name="orderByDesc">Specifies whether to order by descending.</param>
        /// <param name="tracking">Specifies whether to track changes.</param>
        /// <returns>An enumerable of ordered entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            try
            {
                return orderByDesc ? await GetAllActives(tracking).OrderByDescending(orderBy).ToListAsync() : await GetAllActives(tracking).OrderBy(orderBy).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during GetAllAsync with ordering: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all entities that match the specified condition and are ordered by the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to order by.</typeparam>
        /// <param name="expression">The condition to filter entities.</param>
        /// <param name="orderBy">The key selector to order by.</param>
        /// <param name="orderByDesc">Specifies whether to order by descending.</param>
        /// <param name="tracking">Specifies whether to track changes.</param>
        /// <returns>An enumerable of filtered and ordered entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            try
            {
                var values = GetAllActives(tracking).Where(expression);

                return orderByDesc ? await values.OrderByDescending(orderBy).ToListAsync() : await values.OrderBy(orderBy).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during GetAllAsync with condition and ordering: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves the first entity that matches the specified condition.
        /// </summary>
        /// <param name="expression">The condition to filter entities.</param>
        /// <param name="tracking">Specifies whether to track changes.</param>
        /// <returns>The first matching entity, or null if no entity matches.</returns>
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            try
            {
                return await GetAllActives(tracking).FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during GetAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="tracking">Specifies whether to track changes.</param>
        /// <returns>The entity with the specified identifier, or null if no entity matches.</returns>
        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
        {
            try
            {
                return await GetAllActives(tracking).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during GetByIdAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously saves all changes made in the context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> SaveChangeAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during SaveChangeAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Saves all changes made in the context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during SaveChanges: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>The updated entity.</returns>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                return await Task.FromResult(_table.Update(entity).Entity);
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during UpdateAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets all active entities, optionally with or without change tracking.
        /// </summary>
        /// <param name="tracking">Specifies whether to track changes.</param>
        /// <returns>An IQueryable of active entities.</returns>
        protected IQueryable<TEntity> GetAllActives(bool tracking = true)
        {
            var values = _table.Where(x => x.Status != Status.Deleted);

            return tracking ? values : values.AsNoTracking();
        }
    }
}