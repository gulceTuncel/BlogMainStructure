using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.DataAccess.Interfaces;


namespace BlogMainStructure.Infrastructure.Repositories.ArticleRepositories
{
    // Defines the contract for the repository that manages Article entities.
    // Extends various repository interfaces to support async operations.
    public interface IArticleRepository : IRepository, IAsyncInsertableRepository<Article>, IAsyncFindableRepository<Article>, IAsyncOrderableRepository<Article>, IAsyncQueryableRepository<Article>, IAsyncRepository, IAsyncUpdatableRepository<Article>, IAsyncDeletableRepository<Article>
    {
    }
}
