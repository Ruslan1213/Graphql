namespace GraphQlLibary.Domain.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public int CloudStoreId { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
