using BlogMainStructure.Business.DTOs.AuthorDTOs;
using BlogMainStructure.Business.DTOs.TagDTOs;
using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Domain.Utilities.Concretes;
using BlogMainStructure.Domain.Utilities.Interfaces;
using BlogMainStructure.Infrastructure.Repositories.ArticleRepositories;
using BlogMainStructure.Infrastructure.Repositories.TagRepositories;
using Mapster;

namespace BlogMainStructure.Business.Services.TagServices
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IArticleRepository _articleRepository;

        public TagService(ITagRepository tagRepository, IArticleRepository articleRepository)
        {
            _tagRepository = tagRepository;
            _articleRepository = articleRepository;
        }

        public async Task<IResult> AddAsync(TagCreateDTO tagCreateDTO)
        {
            if (await _tagRepository.AnyAsync(x => x.Name.ToLower() == tagCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Tag is already added");
            }
            try
            {
                var newTag = tagCreateDTO.Adapt<Tag>();
                await _tagRepository.AddAsync(newTag);
                await _tagRepository.SaveChangeAsync();

                return new SuccessResult("Tag added successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("An error occurred while adding the tag: " + ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            if (await _articleRepository.AnyAsync(a => a.TagId == id))
            {
                return new ErrorResult("Tag is in use");
            }
            var tagToDelete = await _tagRepository.GetByIdAsync(id);

            if (tagToDelete == null)
            {
                return new ErrorResult("Tag not found in the database.");
            }

            await _tagRepository.DeleteAsync(tagToDelete);
            await _tagRepository.SaveChangeAsync();

            return new SuccessResult("Tag deleted successfully.");
        }

        public async Task<IDataResult<List<TagListDTO>>> GetAllAsync()
        {
            try
            {
                var tags = await _tagRepository.GetAllAsync();

                var tagListDTOs = tags.Adapt<List<TagListDTO>>();

                if (tags.Count() <= 0)
                {
                    return new ErrorDataResult<List<TagListDTO>>(tagListDTOs, "No tag to be listed");
                }

                return new SuccessDataResult<List<TagListDTO>>(tagListDTOs, "Tag listing successful.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TagListDTO>>("An error occurred while retrieving all tags: " + ex.Message);
            }
        }

        public async Task<IDataResult<TagDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var tag = await _tagRepository.GetByIdAsync(id);
                var tagDTO = tag.Adapt<TagDTO>();

                if (tag == null)
                {
                    return new ErrorDataResult<TagDTO>(tagDTO, "Tag not found in the database.");
                }

                return new SuccessDataResult<TagDTO>(tagDTO, "Tag retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TagDTO>("An error occurred while retrieving the tag by ID: " + ex.Message);
            }
        }
    }
}
