using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDetailDto GetAuthorById(int id);
        IEnumerable<NewsItemDto> GetAllNewsItemsByAuthorId(int id);
        bool LinkNewsItemByAuthorIdAndNewsItemId(int authorId, int newsItemId);
        int CreateAuthor(AuthorInputModel author);
        void UpdateAuthor(int id, AuthorInputModel authorData);
        void DeleteAuthor(int id);
    }
}