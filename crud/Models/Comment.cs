using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace MyWebApi.Models;
public class Comment
{
    public int CommentId { get; set; }
    
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public Post Post { get; set; }
        public int PostId { get; set; }
}

