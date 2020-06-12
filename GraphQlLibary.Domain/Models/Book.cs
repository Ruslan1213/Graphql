using System.Collections.Generic;

namespace GraphQlLibary.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Descriprtion { get; set; }

        public int AuthorId { get; set; }

        public virtual ICollection<BookReader> BookReader { get; set; }

        public virtual Author Author { get; set; }
    }
}
