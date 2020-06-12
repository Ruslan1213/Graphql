using System.Collections.Generic;

namespace GraphQlLibary.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
