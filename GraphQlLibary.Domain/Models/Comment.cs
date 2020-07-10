namespace GraphQlLibary.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int ParentCommentId { get; set; }

        public string Text { get; set; }

        public string CommentatorName { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
