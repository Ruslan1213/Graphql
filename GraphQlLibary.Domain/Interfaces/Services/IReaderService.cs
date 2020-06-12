using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface IReaderService
    {
        void Insert(Reader reader);

        void Delete(Reader reader);

        void Update(Reader reader);

        IEnumerable<Reader> Filter(Expression<Func<Reader, bool>> filter);

        IEnumerable<Reader> GetAll();

        Reader Get(string id);

        bool IsExist(Expression<Func<Reader, bool>> filter);

        Reader GetByMail(string mail);

        Reader GetByMailAndPassword(string mail, string password);
    }
}
