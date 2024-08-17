using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BlogMainStructure.Domain.Core.BaseEntities;
using BlogMainStructure.Domain.Enums;
using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.Configurations;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogMainStructure.Infrastructure.AppContext
{
    /// <summary>
    /// Represents the application database context, inheriting from IdentityDbContext for ASP.NET Core Identity.
    /// </summary>
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with HTTP context accessor.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor to get the current user information.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets or sets the DbSet for Author entities.
        /// </summary>
        public virtual DbSet<Tag> Authors { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Article entities.
        /// </summary>
        public virtual DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Author entities.
        /// </summary>
        public virtual DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Configures the model and applies entity configurations from the assembly.
        /// </summary>
        /// <param name="builder">The model builder to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Saves the changes made in the context to the database with base properties set.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges()
        {
            try
            {
                SetBaseProperties();
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during SaveChanges: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously saves the changes made in the context to the database with base properties set.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous save operation.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                SetBaseProperties();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during SaveChangesAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sets base properties for entities based on their state.
        /// </summary>
        private void SetBaseProperties()
        {
            try
            {
                var entries = ChangeTracker.Entries<BaseEntity>();
                var userId = GetUserId();

                foreach (var entry in entries)
                {
                    SetIfAdded(entry, userId);
                    SetIfModified(entry, userId);
                    SetIfDeleted(entry, userId);
                }
            }
            catch (Exception ex)
            {
                // Handle error or log it
                Console.WriteLine($"Error during SetBaseProperties: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sets properties for entities marked as deleted.
        /// </summary>
        /// <param name="entry">The entity entry being processed.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State != EntityState.Deleted)
            {
                return;
            }

            if (entry.Entity is not AuditableEntity entity)
            {
                return;
            }

            entry.State = EntityState.Modified;
            entry.Entity.Status = Status.Deleted;
            entity.DeletedBy = userId;
            entity.DeletedDate = DateTime.Now;
        }

        /// <summary>
        /// Sets properties for entities marked as modified.
        /// </summary>
        /// <param name="entry">The entity entry being processed.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.Status = Status.Modified;
                entry.Entity.UpdatedBy = userId;
                entry.Entity.UpdatedDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Sets properties for entities marked as added.
        /// </summary>
        /// <param name="entry">The entity entry being processed.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Status = Status.Added;
                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Gets the ID of the currently authenticated user.
        /// </summary>
        /// <returns>The user ID or "UserNotFound" if the user cannot be determined.</returns>
        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "UserNotFound";
        }
    }
}
