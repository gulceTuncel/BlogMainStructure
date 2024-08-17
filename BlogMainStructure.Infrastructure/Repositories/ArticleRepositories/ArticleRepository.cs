using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.AppContext;
using BlogMainStructure.Infrastructure.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;


namespace BlogMainStructure.Infrastructure.Repositories.ArticleRepositories
{
    // Repository for managing Article entities.
    // Inherits from EFBaseRepository to provide common data access operations.
    public class ArticleRepository : EFBaseRepository<Article>, IArticleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="AppDbContext"/> instance to use for data access.</param>
        public ArticleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
