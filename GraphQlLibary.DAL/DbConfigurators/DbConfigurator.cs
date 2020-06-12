using GraphQlLibary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlLibary.DAL.DbConfigurators
{
    public class DbConfigurator
    {
        private const int length = 150;

        public static void ConfigureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(x => x.Id);
            modelBuilder.Entity<Author>().HasKey(x => x.Id);
            modelBuilder.Entity<Reader>().HasKey(x => x.Id);
            modelBuilder.Entity<BookReader>().HasKey(x => new { x.BookId, x.ReaderId });

            modelBuilder.Entity<Book>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Reader>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>().HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId);
            modelBuilder.Entity<BookReader>().HasOne(x => x.Book).WithMany(x => x.BookReader).HasForeignKey(x => x.BookId);
            modelBuilder.Entity<BookReader>().HasOne(x => x.Reader).WithMany(x => x.BookReader).HasForeignKey(x => x.ReaderId);

            modelBuilder.Entity<Book>().Property(x => x.Name).HasMaxLength(length);
            modelBuilder.Entity<Book>().Property(x => x.Descriprtion).HasMaxLength(length);
            modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(length);
            modelBuilder.Entity<Reader>().Property(x => x.Name).HasMaxLength(length);
            modelBuilder.Entity<Reader>().Property(x => x.Email).HasMaxLength(length);
            modelBuilder.Entity<Reader>().Property(x => x.Password).HasMaxLength(1000);
        }
    }
}
