using GraphQlLibary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlLibary.DAL.DbConfigurators
{
    public class DbConfigurator
    {
        private const int length = 150;
        private const int lengthBig = 1000;

        public static void ConfigureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);
            modelBuilder.Entity<Post>().HasKey(x => x.Id);
            modelBuilder.Entity<Photo>().HasKey(x => x.Id);
            modelBuilder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });
            modelBuilder.Entity<Like>().HasKey(x => new { x.PostId, x.UserId });

            modelBuilder.Entity<User>().HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Photo>().HasOne(x => x.Post).WithMany(x => x.Photos).HasForeignKey(x => x.PostId);
            modelBuilder.Entity<Post>().HasMany(x => x.Comments).WithOne(x => x.Post).HasForeignKey(x => x.PostId);

            modelBuilder.Entity<UserRole>().HasOne(x => x.Role).WithMany(x => x.UserRole).HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<UserRole>().HasOne(x => x.User).WithMany(x => x.UserRole).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Like>().HasOne(x => x.Post).WithMany(x => x.Likes).HasForeignKey(x => x.PostId);
            modelBuilder.Entity<Like>().HasOne(x => x.User).WithMany(x => x.Likes).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Role>().Property(x => x.Name).HasMaxLength(length);
            modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(length);
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(length);
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(lengthBig);
            modelBuilder.Entity<Comment>().Property(x => x.Text).HasMaxLength(lengthBig);
        }
    }
}
