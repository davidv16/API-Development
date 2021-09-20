using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleanThatCode.Community.Repositories.Implementations;
using CleanThatCode.Community.Repositories.Interfaces;
using CleanThatCode.Community.Tests;
using CleanThatCode.Community.Tests.Mocks;

namespace CleanThatCode.Community.Tests
{
    [TestClass]
    public class CommentRepositiryTests
    {
        private ICommentRepository _commentRepository;

        [TestInitialize]
        public void Initialize()
        {
            _commentRepository = new CommentRepository(new CleanThatCodeDbContextMock());
        }

        [TestMethod]
        public void GetAllCommentsByPostId_GivenWrongPostId_ShouldReturnNoComments()
        {
            var comments = _commentRepository.GetAllCommentsByPostId(0);
            Assert.AreEqual(0, comments.Count());            
        }

        [TestMethod]
        public void GetAllCommentsByPostId_GivenValidPostId_ShouldReturnTwoComments()
        {
            var comments = _commentRepository.GetAllCommentsByPostId(1);
            Assert.AreEqual(2, comments.Count());            
        }
    }
}