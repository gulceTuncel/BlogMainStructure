using BlogMainStructure.Business.DTOs.ArticleDTOs;
using BlogMainStructure.Domain.Utilities.Interfaces;

namespace BlogMainStructure.Business.Services.ArticleServices
{
    public interface IArticleService
    {
        Task<IResult> AddAsync(ArticleCreateDTO articleCreateDTO);
        Task<IDataResult<List<ArticleListDTO>>> GetAllAsync();
        Task<IDataResult<List<ArticleListDTO>>> GetAllAsync(Guid authorId);
        Task<IDataResult<ArticleDTO>> GetByIdAsync(Guid id);
        Task<IResult> DeleteAsync(Guid id, Guid authorId);
        Task<IDataResult<List<ArticleListDTO>>> Top5GetAll();
    }
}
