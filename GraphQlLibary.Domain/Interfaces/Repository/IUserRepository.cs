using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Domain.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByMail(string mail);

        User GetByMailAndPassword(string mail, string password);
    }
}
