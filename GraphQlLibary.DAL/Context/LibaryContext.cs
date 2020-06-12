using GraphQlLibary.DAL.DbConfigurators;
using GraphQlLibary.DAL.DbInitializers;
using GraphQlLibary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlLibary.DAL.Context
{
    public class LibaryContext : DbContext
    {
        public LibaryContext(DbContextOptions<LibaryContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Reader> Readers { get; set; }

        public DbSet<BookReader> BookReaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbConfigurator.ConfigureDb(modelBuilder);
            DbInitializator.InitializeDb(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
    }
}
