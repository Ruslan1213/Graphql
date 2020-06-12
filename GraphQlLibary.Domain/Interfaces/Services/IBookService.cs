using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface IBookService
    {
        void Insert(Book book);

        void Delete(Book book);

        void Update(Book book);

        IEnumerable<Book> Filter(Expression<Func<Book, bool>> filter);

        IEnumerable<Book> GetAll();

        Book Get(string id);

        bool IsExist(Expression<Func<Book, bool>> filter);

        Book GetByDescription(string description);
    }
}
