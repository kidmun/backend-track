using Xunit;
using Moq;
using MyWebApi.Controllers;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Tests
{
    public class CommentTests
    {
        [Fact]
        public void CreateComment_ShouldCreateComment()
        {
           
            var mockDbContext = new Mock<ApiDbContext>();
            var commentController = new CommentController(mockDbContext.Object);
            var newComment = new Comment
            {
                PostId = 1,
                CommentId = 2, 
                Text = "Test Comment"
            };

            var result = commentController.CommentAsync(newComment.PostId, newComment.CommentId, newComment.Text);

            Assert.NotNull(result);
          
        }
    }
}
