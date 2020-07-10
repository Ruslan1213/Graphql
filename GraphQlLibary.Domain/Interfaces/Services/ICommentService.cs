using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        void Insert(Comment comment);

        void Delete(Comment comment);

        void Update(Comment comment);

        IEnumerable<Comment> Filter(Expression<Func<Comment, bool>> filter);

        IEnumerable<Comment> GetAll();

        Comment Get(int id);

        bool IsExist(Expression<Func<Comment, bool>> filter);
    }
}
