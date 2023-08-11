using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebApi.Models;
using MyWebApi.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

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
public async Task<IActionResult> CommentAsync(CreateCommentDto newComment)
{  

   
        Post post = await _context.Posts.FindAsync(newComment.PostId);

        if (post == null)
        {
            return NotFound("Associated post not found");
        }
        
            var newComm = new Comment
            {
                Text = newComment.Text,
                CommentId =  newComment.CommentId,
                Post= post,
                CreatedAt =  DateTime.UtcNow
            };

        _context.Comments.Add(newComm);
        _context.SaveChangesAsync();

          return Ok(newComm);
    
        }
 [HttpPatch]
    public async Task<IActionResult> Patch(int id, string text = null)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(comment => comment.CommentId == id);

        if (comment == null)
        {
            return BadRequest(error: "can't find the comment");
        }
        if (text != null)
        {
            comment.Text = text;
        }
        

        await _context.SaveChangesAsync();


        return Ok("successfully updated the comment");

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