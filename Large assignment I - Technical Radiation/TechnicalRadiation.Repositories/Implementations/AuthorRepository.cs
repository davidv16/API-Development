using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Contexts;
using TechnicalRadiation.Repositories.Interfaces;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly NewsDbContext _dbContext;

        public AuthorRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _dbContext.Authors.Select(n => new AuthorDto
            {
                Id = n.Id,
                Name = n.Name
            }).ToList();

            foreach (var author in authors)
            {
                author.Links.AddReference("self", new { href = $"api/authors/{author.Id}" });
                author.Links.AddReference("edit", new { href = $"api/authors/{author.Id}" });
                author.Links.AddReference("delete", new { href = $"api/authors/{author.Id}" });
                author.Links.AddReference("newsItems", new { href = $"api/authors/{author.Id}/newsItems" });
                author.Links
                    .AddListReference(
                        "newsItemsDetailed",
                        authors
                            .Where(a => a.Id == author.Id)
                            .Join(
                                _dbContext.AuthorNewsItem,
                                a => a.Id,
                                ani => ani.AuthorsId,
                                (a, ani) => new { a, ani }
                            )
                            .Join(
                                _dbContext.NewsItems,
                                a_ani => a_ani.ani.NewsItemsId,
                                ni => ni.Id,
                                (a_ani, ni) => new { ni }
                            )
                            .Select(d => new { href = $"api/{d.ni.Id}" })
                    );
            }

            return authors;
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = _dbContext.Authors.Where(n => n.Id == id).Select(n => new AuthorDetailDto
            {
                Id = n.Id,
                Name = n.Name,
                ProfileImgSource = n.ProfileImgSource,
                Bio = n.Bio
            }).FirstOrDefault();

            if (author != null)
            {
                author.Links.AddReference("self", new { href = $"api/authors/{author.Id}" });
                author.Links.AddReference("edit", new { href = $"api/authors/{author.Id}" });
                author.Links.AddReference("delete", new { href = $"api/authors/{author.Id}" });
                author.Links.AddReference("newsItems", new { href = $"api/authors/{author.Id}/newsItems" });
                author.Links
                    .AddListReference(
                        "newsItemsDetailed",
                        _dbContext.Authors
                            .Where(a => a.Id == author.Id)
                            .Join(
                                _dbContext.AuthorNewsItem,
                                a => a.Id,
                                ani => ani.AuthorsId,
                                (a, ani) => new { a, ani }
                            )
                            .Join(
                                _dbContext.NewsItems,
                                a_ani => a_ani.ani.NewsItemsId,
                                ni => ni.Id,
                                (a_ani, ni) => new { ni }
                            )
                            .Select(d => new { href = $"api/{d.ni.Id}" })
                    );
            }

            return author;
        }

        public IEnumerable<NewsItemDto> GetAllNewsItemsByAuthorId(int id)
        {
            var newsItems = _dbContext.Authors
                .Where(d => d.Id == id)
                .Join(_dbContext.AuthorNewsItem,
                    a => a.Id,
                    ani => ani.AuthorsId,
                    (a, ani) => new { a, ani }
                )
                .Join(_dbContext.NewsItems,
                    a_ani => a_ani.ani.NewsItemsId,
                    ni => ni.Id,
                    (a_ani, ni) => new { author = a_ani.a, newsitem = ni }
                )
                .Select(d => new NewsItemDto
                {
                    Id = d.newsitem.Id,
                    Title = d.newsitem.Title,
                    ImgSource = d.newsitem.ImgSource,
                    ShortDescription = d.newsitem.ShortDescription
                }).ToList();

            // Adding HyperMedia
            foreach (var newsItem in newsItems)
            {
                newsItem.Links.AddReference("self", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddReference("edit", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddReference("delete", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddListReference("authors", _dbContext.AuthorNewsItem
                    .Where(ani => ani.NewsItemsId == newsItem.Id)
                    .Select(ani => new { href = $"api/authors/{ani.AuthorsId}" })
                );
                newsItem.Links.AddListReference("categories", _dbContext.CategoryNewsItem
                    .Where(cni => cni.NewsItemsId == newsItem.Id)
                    .Select(cni => new { href = $"api/categories/{cni.CategoriesId}" })
                );
            }

            return newsItems;
        }

        public bool LinkNewsItemByAuthorIdAndNewsItemId(int authorId, int newsItemId)
        {
            var authorExists = _dbContext.Authors.Where(n => n.Id == authorId).FirstOrDefault();
            var newsItemExists = _dbContext.NewsItems.Where(n => n.Id == newsItemId).FirstOrDefault();
            if(authorExists == null || newsItemExists == null)
            {
                return false;
            }
            var author = _dbContext.Authors
                .Where(a => a.Id == authorId)
                .FirstOrDefault();

            var newsItem = _dbContext.NewsItems
                .Where(ni => ni.Id == newsItemId)
                .FirstOrDefault();

            var authorNewsItem = _dbContext.AuthorNewsItem
                .Where(ani => ani.AuthorsId == authorId && ani.NewsItemsId == newsItemId)
                .FirstOrDefault();

            if (author != null && newsItem != null && authorNewsItem == null)
            {
                _dbContext.AuthorNewsItem.Add(new AuthorNewsItem
                {
                    AuthorsId = authorId,
                    NewsItemsId = newsItemId
                });

                _dbContext.SaveChanges();
            }
            return true;            
        }

        public int CreateAuthor(AuthorInputModel author)
        {
            var nextId = _dbContext.Authors.Max(table => table.Id) + 1;
            _dbContext.Authors.Add(new Author
            {
                // code generated
                Id = nextId,
                // from body
                Name = author.Name,
                ProfileImgSource = author.ProfileImgSource,
                Bio = author.Bio,
                
                // code generated
                ModifiedBy = "David",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
                
            });

            _dbContext.SaveChanges();

            return nextId;
        }

        public void UpdateAuthor(int id, AuthorInputModel authorData)
        {
            var singleAuthor = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            
            singleAuthor.Name = authorData.Name;
            singleAuthor.ProfileImgSource = authorData.ProfileImgSource;
            singleAuthor.Bio = authorData.Bio;
            
            _dbContext.SaveChanges();
        }
        
        public void DeleteAuthor(int id)
        {
            var author = _dbContext.Authors.Single(n => n.Id == id);
            _dbContext.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}