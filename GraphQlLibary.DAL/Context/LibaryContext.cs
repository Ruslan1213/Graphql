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
             Database.EnsureCreated();
        }

        public DbSet<Role> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbConfigurator.ConfigureDb(modelBuilder);
            DbInitializator.InitializeDb(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseLazyLoadingProxies();
    }
}
