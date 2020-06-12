using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using System.Linq;

namespace GraphQlLibary.DAL.Repositories
{
    public class ReaderRepository : Repository<Reader>, IReaderRepository
    {
        private readonly LibaryContext _libaryContext;

        public ReaderRepository(LibaryContext libaryContext) : base(libaryContext)
        {
            _libaryContext = libaryContext;
        }

        public Reader GetByMail(string mail)
        {
            return _libaryContext.Readers.FirstOrDefault(x => x.Email == mail);
        }

        public Reader GetByMailAndPassword(string mail, string password)
        {
            return _libaryContext.Readers.FirstOrDefault(x => x.Email == mail && x.Password == password);
        }
    }
}
