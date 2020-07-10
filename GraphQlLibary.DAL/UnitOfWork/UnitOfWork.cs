using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRoleRepository AuthorRepository { get; }

        public IPostRepository BookRepository { get; }

        public IUserRepository ReaderRepository { get; }

        public IPostRepository PostRepository { get; }

        public IRepository<Photo> PhotoRepository { get; }

        public IRepository<Comment> CommentRepository { get; }

        private readonly LibaryContext _libaryContext;

        public UnitOfWork(
            IRepository<Photo> photoRepository,
            IRepository<Comment> commentRepository,
            IRoleRepository authorRepository,
            IPostRepository bookRepository,
            IUserRepository readerRepository,
            IPostRepository postRepository,
            LibaryContext libaryContext)
        {
            _libaryContext = libaryContext;
            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
            PostRepository = postRepository;
            ReaderRepository = readerRepository;
            PhotoRepository = photoRepository;
            CommentRepository = commentRepository;
        }

        public void Commit()
        {
            _libaryContext.SaveChanges();
        }
    }
}
