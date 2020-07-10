using System.Collections.Generic;

namespace GraphQlLibary.Domain.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
