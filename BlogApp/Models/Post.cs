namespace MyWebApi.Models;

public class Post
{
     public Post()
    {
        Comments = new List<Comment>();
    }

    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
public ICollection<Comment> Comments { get; set; }
 
}
