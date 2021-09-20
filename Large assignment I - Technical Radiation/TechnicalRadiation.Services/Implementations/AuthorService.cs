using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();

            return authors;
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);

            return author;
        }
        public IEnumerable<NewsItemDto> GetAllNewsItemsByAuthorId(int id)
        {
            var newsItems = _authorRepository.GetAllNewsItemsByAuthorId(id);

            return newsItems;
        }
        
        public bool LinkNewsItemByAuthorIdAndNewsItemId(int authorId, int newsItemId)
        {
            return _authorRepository.LinkNewsItemByAuthorIdAndNewsItemId(authorId, newsItemId);
        }

        public int CreateAuthor(AuthorInputModel author)
        {
            return _authorRepository.CreateAuthor(author);
        }

        public void UpdateAuthor(int id, AuthorInputModel authorData)
        {
            _authorRepository.UpdateAuthor(id, authorData);
        }

        public void DeleteAuthor(int id)
        {
            _authorRepository.DeleteAuthor(id);
        }
        
    }
}