using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.AppContext;
using BlogMainStructure.Infrastructure.DataAccess.EntityFramework;

namespace BlogMainStructure.Infrastructure.Repositories.AuthorRepositories
{
    // Repository for managing Author entities.
    // Inherits from EFBaseRepository to provide common data access operations.
    public class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="AppDbContext"/> instance to use for data access.</param>
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }

}
