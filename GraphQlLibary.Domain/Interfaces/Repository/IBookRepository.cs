using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Domain.Interfaces.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetByDescription(string description);
    }
}
