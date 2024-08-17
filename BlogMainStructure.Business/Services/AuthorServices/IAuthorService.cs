using BlogMainStructure.Business.DTOs.AuthorDTOs;
using BlogMainStructure.Domain.Utilities.Interfaces;


namespace BlogMainStructure.Business.Services.AuthorServices
{
    // Interface for user-related operations in the application.
    public interface IAuthorService
    {
        Task<IResult> AddAsync(AuthorCreateDTO AuthorCreateDTO);
        Task<IResult> UpdateAsync(AuthorUpdateDTO AuthorUpdateDTO);
        Task<IDataResult<List<AuthorListDTO>>> GetAllAsync();
        Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id);
        Task<Guid> GetAuthorIdByEmail(string email);
        Task<IResult> DeleteAsync(Guid id);
    }
}
