using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebApi.Models;
using MyWebApi.Data;

namespace MyWebApi.Controllers;
[Route(template: "api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private static ApiDbContext _context;
    public PostController(ApiDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetPostsAsync()
    {
        var posts = _context.Posts.ToList();

    foreach (var post in posts)
    {
        var comments = _context.Comments.Where(c => c.PostId == post.PostId).ToList();
        post.Comments = comments;
    }

        return Ok(posts);
    }

    [HttpGet(template: "{id}")]
    public async Task<IActionResult> Get(int id)
    {

        var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
        var comments = _context.Comments.Where(c => c.PostId == id).ToList();
        post.Comments = comments;
        if (post == null)
        {
            return BadRequest(error: "there is no post with this id");
        }
        return Ok(post);
    }
    [HttpPost]
    public async Task<IActionResult> Post(Post post)
    {

        _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return Ok("sucesss");
    }
    [HttpPatch]
    public async Task<IActionResult> Patch(int id, string title = null, string content = null)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(post => post.PostId == id);

        if (post == null)
        {
            return BadRequest(error: "can't find the post");
        }
        if (title != null)
        {
            post.Title = title;
        }
        if (content != null)
        {
            post.Content = content;
        }

        await _context.SaveChangesAsync();


        return Ok("successfully updated the post");

    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
        if (post == null)
        {
            return BadRequest(error: "can't find the post");
        }
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return Ok("Succcessfully removed");
    }
}