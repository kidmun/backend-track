using Xunit;
using Moq;
using MyWebApi.Controllers;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.tests;

    public class PostTests
    {
        [Fact]
        public void CreatePost_ShouldCreatePost()
        {
            var mockDbContext = new Mock<ApiDbContext>();
            var postManager = new PostController(mockDbContext.Object);
            var newPost = new Post
            {
                Title = "Test Post",
                Content = "This is a test post content."
            };
            var result = postManager.Post(newPost);
            Assert.NotNull(result);
       
        }
    }

