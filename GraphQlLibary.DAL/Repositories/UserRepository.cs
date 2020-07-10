using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using System.Linq;

namespace GraphQlLibary.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly LibaryContext _libaryContext;

        public UserRepository(LibaryContext libaryContext) : base(libaryContext)
        {
            _libaryContext = libaryContext;
        }

        public User GetByMail(string mail)
        {
            return _libaryContext.Users.FirstOrDefault(x => x.Email == mail);
        }

        public User GetByMailAndPassword(string mail, string password)
        {
            return _libaryContext.Users.FirstOrDefault(x => x.Email == mail && x.Password == password);
        }
    }
}
