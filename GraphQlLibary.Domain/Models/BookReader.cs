namespace GraphQlLibary.Domain.Models
{
    public class BookReader
    {
        public int BookId { get; set; }

        public int ReaderId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Reader Reader { get; set; }
    }
}
