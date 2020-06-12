using GraphQlLibary.DAL.Extensions;
using GraphQlLibary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlLibary.DAL.DbInitializers
{
    public class DbInitializator
    {
        public static void InitializeDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Name = "Война и мир", Descriprtion = "Книга о войне и мире", AuthorId = 1 },
                new Book { Id = 2, Name = "Алые паруса", Descriprtion = "Книга о девочке с добрым сердцем которая умела ждать и верить в чудо", AuthorId = 2 });

            modelBuilder.Entity<Author>().HasData(
                 new Author { Id = 1, Name = "Лев Толстой" },
                 new Author { Id = 2, Name = "Александ Грин" }
                );

            modelBuilder.Entity<Reader>().HasData(
                new Reader { Id = 1, Name = "Руслан", Email = "ruslanvolodko3@gmail.com", Password = "qwerty123@".GetHashString() });

            modelBuilder.Entity<BookReader>().HasData(
                new BookReader { ReaderId = 1, BookId = 1 },
                new BookReader { ReaderId = 1, BookId = 2 });
        }
    }
}
