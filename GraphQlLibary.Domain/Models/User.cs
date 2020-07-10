using System.Collections.Generic;

namespace GraphQlLibary.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
