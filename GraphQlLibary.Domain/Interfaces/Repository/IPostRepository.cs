using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Domain.Interfaces.Repository
{
    public interface IPostRepository : IRepository<Post>
    {
        Post GetByLikes(int likesCount);

        void AddLike(Like like);
    }
}
