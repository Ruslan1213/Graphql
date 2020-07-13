using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface IPostService
    {
        void Insert(Post book);

        void Delete(Post book);

        void Update(Post book);

        IEnumerable<Post> Filter(Expression<Func<Post, bool>> filter);

        IEnumerable<Post> GetAll();

        Post Get(int id);

        bool IsExist(Expression<Func<Post, bool>> filter);

        Post GetByLikes(int likes);

        bool AddLike(Like like);
    }
}
