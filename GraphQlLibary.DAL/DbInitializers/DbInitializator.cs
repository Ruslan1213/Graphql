using GraphQlLibary.DAL.Extensions;
using GraphQlLibary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlLibary.DAL.DbInitializers
{
    public class DbInitializator
    {
        public static void InitializeDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin"},
                new Role { Id = 2, Name = "User"});

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Руслан", Email = "ruslanvolodko3@gmail.com", Password = "qwerty123@".GetHashString() });

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 1, RoleId = 2 });
        }
    }
}
