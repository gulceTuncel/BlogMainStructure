using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.AppContext;
using BlogMainStructure.Infrastructure.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BlogMainStructure.Infrastructure.Repositories.TagRepositories
{
    public class TagRepository : EFBaseRepository<Tag>, ITagRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="AppDbContext"/> instance to use for data access.</param>
        public TagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
