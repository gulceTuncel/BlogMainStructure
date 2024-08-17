using BlogMainStructure.Business.DTOs.ArticleDTOs;
using BlogMainStructure.Business.DTOs.AuthorDTOs;
using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Domain.Utilities.Concretes;
using BlogMainStructure.Domain.Utilities.Interfaces;
using BlogMainStructure.Infrastructure.Repositories.ArticleRepositories;
using Mapster;
using System;

namespace BlogMainStructure.Business.Services.ArticleServices
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<IResult> AddAsync(ArticleCreateDTO articleCreateDTO)
        {
            try
            {
                var newArticle = articleCreateDTO.Adapt<Article>();
                await _articleRepository.AddAsync(newArticle);
                await _articleRepository.SaveChangeAsync();

                return new SuccessResult("Article is successfully added");
            }
            catch (Exception ex)
            {

                return new ErrorResult("An error occurred while adding the article: " + ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id, Guid authorId)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);

                if (article is null)
                {
                    return new ErrorResult("Article not found in the database.");
                }

                if (authorId != article.Id)
                {
                    return new ErrorResult("You are not authorized to delete this article");
                }

                await _articleRepository.DeleteAsync(article);
                await _articleRepository.SaveChangeAsync();

                return new SuccessResult("Article is successfully deleted");
            }
            catch (Exception ex)
            {

                return new ErrorResult("An error occurred while deleting the article: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<ArticleListDTO>>> GetAllAsync()
        {
            try
            {
                var articles = await _articleRepository.GetAllAsync();

                var articleListDTOs = articles.Adapt<List<ArticleListDTO>>();

                if (articles.Count() <= 0)
                {
                    return new ErrorDataResult<List<ArticleListDTO>>(articleListDTOs, "No articles to be listed");
                }

                return new SuccessDataResult<List<ArticleListDTO>>(articleListDTOs, "Article listing successful.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ArticleListDTO>>("An error occurred while retrieving all articles: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<ArticleListDTO>>> GetAllAsync(Guid authorId)
        {
            try
            {
                var articles = await _articleRepository.GetAllAsync(x=>x.AuthorId == authorId);

                var articleListDTOs = articles.Adapt<List<ArticleListDTO>>();

                if (articles.Count() <= 0)
                {
                    return new ErrorDataResult<List<ArticleListDTO>>(articleListDTOs, "This author has no published article");
                }

                return new SuccessDataResult<List<ArticleListDTO>>(articleListDTOs, "Article listing successful.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ArticleListDTO>>("An error occurred while retrieving author's articles: " + ex.Message);
            }
        }

        public async Task<IDataResult<ArticleDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                await UpViewCountByArticleId(id);
                var articleDTO = article.Adapt<ArticleDTO>();

                if (article == null)
                {
                    return new ErrorDataResult<ArticleDTO>(articleDTO, "Article not found in the database.");
                }

                return new SuccessDataResult<ArticleDTO>(articleDTO, "Article information retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ArticleDTO>("An error occurred while retrieving the article by ID: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<ArticleListDTO>>> Top5GetAll()
        {
            try
            {
                var articles = (await _articleRepository.GetAllAsync(x => x.ViewCount, true)).Take(5);

                var articleListDTOs = articles.Adapt<List<ArticleListDTO>>();

                return new SuccessDataResult<List<ArticleListDTO>>(articleListDTOs, "Top 5 articles listed successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ArticleListDTO>>("An error occurred while retrieving all articles: " + ex.Message);
            }
        }

        private async Task UpViewCountByArticleId(Guid id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            article.ViewCount++;
            await _articleRepository.UpdateAsync(article);
            await _articleRepository.SaveChangeAsync();
        }
    }
}