namespace MyWebApi
{
    public class CreateCommentDto
    {
      
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}