using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface IUserService
    {
        void Insert(User reader);

        void Delete(User reader);

        void Update(User reader);

        IEnumerable<User> Filter(Expression<Func<User, bool>> filter);

        IEnumerable<User> GetAll();

        User Get(int id);

        bool IsExist(Expression<Func<User, bool>> filter);

        User GetByMail(string mail);

        User GetByMailAndPassword(string mail, string password);
    }
}
