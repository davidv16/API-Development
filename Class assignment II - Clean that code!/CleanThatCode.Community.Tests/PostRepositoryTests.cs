using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleanThatCode.Community.Repositories.Implementations;
using CleanThatCode.Community.Repositories.Interfaces;
using CleanThatCode.Community.Repositories.Data;
using CleanThatCode.Community.Models.Entities;
using FizzWare.NBuilder;

namespace CleanThatCode.Community.Tests
{
    [TestClass]
    public class PostRepositiryTests
    {
        private IPostRepository _postRepository;
        private Mock<ICleanThatCodeDbContext> _dbContextMock = new Mock<ICleanThatCodeDbContext>();

        [TestInitialize]
        public void Initialize()
        {
            _dbContextMock.Setup(method => method.Posts).Returns(
                Builder<Post>
                    .CreateListOfSize(3)
                    .TheFirst(2)
                    .With(x => x.Title = "Greyskull")
                    .With(x => x.Author = "He-Man")
                    .TheLast(1)
                    .With(x => x.Title = "Hack the planet!")
                    .With(x => x.Author = "Richard Stallman")
                    .Build()
            );
            _postRepository = new PostRepository(_dbContextMock.Object);
        }

        [TestMethod]
        public void GetAllPosts_NoFilter_ShouldContainAListOfThree()
        {
            var posts = _postRepository.GetAllPosts("", "");
            Assert.AreEqual(3, posts.Count());            
        }

        [TestMethod]
        public void GetAllPosts_FilteredByTitle_ShouldContainAListOfTwo()
        {
            var posts = _postRepository.GetAllPosts("Greyskull", "");
            Assert.AreEqual(2, posts.Count());            
        }

        [TestMethod]
        public void GetAllPosts_FilteredByAuthor_ShouldContainAListOfOne()
        {
            var posts = _postRepository.GetAllPosts("", "Stallman");
            Assert.AreEqual(1, posts.Count());            
        }
    }
}