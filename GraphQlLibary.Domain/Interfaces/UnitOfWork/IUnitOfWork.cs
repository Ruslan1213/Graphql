using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using System.Data;

namespace GraphQlLibary.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRoleRepository AuthorRepository { get; }

        IUserRepository ReaderRepository { get; }

        IPostRepository PostRepository { get; }

        IRepository<Comment> CommentRepository { get; }

        IRepository<Photo> PhotoRepository { get; }

        void Commit();
    }
}
