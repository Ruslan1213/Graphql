using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Domain.Interfaces.Repository
{
    public interface IReaderRepository : IRepository<Reader>
    {
        Reader GetByMail(string mail);

        Reader GetByMailAndPassword(string mail, string password);
    }
}
