using BlogMainStructure.Business.DTOs.TagDTOs;
using BlogMainStructure.Domain.Utilities.Interfaces;


namespace BlogMainStructure.Business.Services.TagServices
{
    public interface ITagService
    {
        Task<IResult> AddAsync(TagCreateDTO tagCreateDTO);
        Task<IDataResult<List<TagListDTO>>> GetAllAsync();
        Task<IDataResult<TagDTO>> GetByIdAsync(Guid id);
        Task<IResult> DeleteAsync(Guid id);
    }
}
