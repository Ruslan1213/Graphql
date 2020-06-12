using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<Author> InsertAsync(Author author);

        Task<Author> GetAuthorByNameAsync(string name);

        Task<IEnumerable<Author>> GetAllAcync();

        void Insert(Author author);

        void Delete(Author author);

        void Update(Author author);

        IEnumerable<Author> Filter(Expression<Func<Author, bool>> filter);

        IEnumerable<Author> GetAll();

        Author Get(string id);

        bool IsExist(Expression<Func<Author, bool>> filter);

        Author GetByName(string name);
    }
}
