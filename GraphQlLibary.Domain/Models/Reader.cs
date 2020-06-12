using System.Collections.Generic;

namespace GraphQlLibary.Domain.Models
{
    public class Reader
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<BookReader> BookReader { get; set; }
    }
}
