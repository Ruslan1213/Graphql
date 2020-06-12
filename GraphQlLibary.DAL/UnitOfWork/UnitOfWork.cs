using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;

namespace GraphQlLibary.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthorRepository AuthorRepository { get; }

        public IBookRepository BookRepository { get; }

        public IReaderRepository ReaderRepository { get; }

        private readonly LibaryContext _libaryContext;

        public UnitOfWork(
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IReaderRepository readerRepository,
            LibaryContext libaryContext)
        {
            _libaryContext = libaryContext;
            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
            ReaderRepository = readerRepository;
        }

        public void Commit()
        {
            _libaryContext.SaveChanges();
        }
    }
}
