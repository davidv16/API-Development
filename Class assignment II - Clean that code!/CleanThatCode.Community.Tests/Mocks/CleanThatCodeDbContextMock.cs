using System;
using System.Collections.Generic;
using CleanThatCode.Community.Repositories.Data;
using CleanThatCode.Community.Models.Entities;
using CleanThatCode.Community.Tests.Mocks;

namespace CleanThatCode.Community.Tests.Mocks
{
    public class CleanThatCodeDbContextMock : ICleanThatCodeDbContext
    {
        public IEnumerable<Comment> Comments
        {
            get
            {
                return FakeData.Comments;
            }
        }

        public IEnumerable<Post> Posts
        {
            get
            {
                return FakeData.Posts;
            }
        }
    }
}