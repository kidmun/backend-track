using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebApi.Models;
using MyWebApi.Data;

namespace MyWebApi.Controllers;
[Route(template:"api/[controller]")]
[ApiController]
public  class CommentController: ControllerBase
{
    private static ApiDbContext _context;
    public CommentController(ApiDbContext context){
        _context = context;
    }
   

[HttpGet]
 public async Task<IActionResult> GetPostsAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }

[HttpPost]
public async Task<IActionResult> CommentAsync(int postId, int commentId, string text)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
            if (post == null){
                return BadRequest(error: "can't find the post");
            }
            DateTime utcDateTime = DateTime.UtcNow;
            Comment comment = new Comment{
                    PostId= postId,
                    CommentId = commentId,
                    Text = text,
                    CreatedAt = utcDateTime,
                    Post =  post
            };
            post.Comments.Add(comment);
                _context.Comments.AddAsync(comment);
                   await _context.SaveChangesAsync();
            return Ok(
            "success"
            );
        }



[HttpDelete]
public async Task<IActionResult> Delete(int id){
    var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);
    if (comment == null){
        return BadRequest(error: "can't find the comment");
    }
 _context.Comments.Remove(comment);
   await _context.SaveChangesAsync();
    return Ok("Succcessfully removed");
}
}