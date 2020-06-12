using GraphQlLibary.Domain.Interfaces.Repository;

namespace GraphQlLibary.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthorRepository AuthorRepository { get; }

        IBookRepository BookRepository { get; }

        IReaderRepository ReaderRepository { get; }

        void Commit();
    }
}
