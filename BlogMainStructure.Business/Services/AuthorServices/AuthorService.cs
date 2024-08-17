using BlogMainStructure.Domain.Utilities.Concretes;
using Mapster;
using BlogMainStructure.Business.DTOs.AuthorDTOs;
using BlogMainStructure.Domain.Entities;
using BlogMainStructure.Infrastructure.Repositories.AuthorRepositories;
using BlogMainStructure.Domain.Utilities.Interfaces;

namespace BlogMainStructure.Business.Services.AuthorServices
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IResult> AddAsync(AuthorCreateDTO authorCreateDTO)
        {
            if (await _authorRepository.AnyAsync(x => x.Email.ToLower() == authorCreateDTO.Email.ToLower()))
            {
                return new ErrorResult("E-mail is already in use");
            }
            try
            {
                var newAuthor = authorCreateDTO.Adapt<Author>();
                await _authorRepository.AddAsync(newAuthor);
                await _authorRepository.SaveChangeAsync();

                return new SuccessResult("Author added successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("An error occurred while adding the author: " + ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var authorToDelete = await _authorRepository.GetByIdAsync(id);

                if (authorToDelete == null)
                {
                    return new ErrorResult("Author not found in the database.");
                }

                await _authorRepository.DeleteAsync(authorToDelete);
                await _authorRepository.SaveChangeAsync();

                return new SuccessResult("Author deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("An error occurred while deleting the author: " + ex.Message);
            }
        }

        public async Task<IDataResult<List<AuthorListDTO>>> GetAllAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();

                var authorListDTOs = authors.Adapt<List<AuthorListDTO>>();

                if (authors.Count() <= 0)
                {
                    return new ErrorDataResult<List<AuthorListDTO>>(authorListDTOs, "No author to be listed");
                }

                return new SuccessDataResult<List<AuthorListDTO>>(authorListDTOs, "Author listing successful.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AuthorListDTO>>("An error occurred while retrieving all authors: " + ex.Message);
            }
        }

        public async Task<Guid> GetAuthorIdByEmail(string email)
        {
           var author = await _authorRepository.GetAsync(x => x.Email == email);

            if(author is null)
            {
                return Guid.Empty;
            }

            return author.Id;
        }

        public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                var authorDTO = author.Adapt<AuthorDTO>();

                if (author == null)
                {
                    return new ErrorDataResult<AuthorDTO>(authorDTO, "Author not found in the database.");
                }
                
                return new SuccessDataResult<AuthorDTO>(authorDTO, "Author information retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AuthorDTO>("An error occurred while retrieving the author by ID: " + ex.Message);
            }
        }

        public async Task<IResult> UpdateAsync(AuthorUpdateDTO authorUpdateDTO)
        {
            try
            {
                var authorToUpdate = await _authorRepository.GetByIdAsync(authorUpdateDTO.Id);

                if (authorToUpdate == null)
                {
                    return new ErrorResult("Author not found in the database.");
                }

                var updated = authorUpdateDTO.Adapt(authorToUpdate);
                await _authorRepository.UpdateAsync(updated);
                await _authorRepository.SaveChangeAsync();

                return new SuccessResult("Author updated successfully.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("An error occurred while updating the author: " + ex.Message);
            }
        }
    }
}
