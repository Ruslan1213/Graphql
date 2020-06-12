using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using System.Linq;

namespace GraphQlLibary.DAL.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly LibaryContext _libaryContext;

        public BookRepository(LibaryContext libaryContext) : base(libaryContext)
        {
            _libaryContext = libaryContext;
        }

        public Book GetByDescription(string description)
        {
            return _libaryContext.Books.FirstOrDefault(x => x.Descriprtion == description);
        }
    }
}
