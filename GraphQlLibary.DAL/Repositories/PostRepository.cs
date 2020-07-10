using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using System.Linq;

namespace GraphQlLibary.DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly LibaryContext _libaryContext;

        public PostRepository(LibaryContext libaryContext) : base(libaryContext)
        {
            _libaryContext = libaryContext;
        }

        public Post GetByLikes(int likesCount)
        {
            return _libaryContext.Posts.FirstOrDefault(x => x.Likes == likesCount);
        }
    }
}
