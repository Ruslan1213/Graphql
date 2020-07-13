using System;
using System.Collections.Generic;

namespace GraphQlLibary.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }

        public DateTime DateOfPost { get; set; }

        public string Description { get; set; }

        public string PhotoUri { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}
