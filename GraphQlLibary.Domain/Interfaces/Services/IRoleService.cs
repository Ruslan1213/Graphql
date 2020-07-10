using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface IRoleService
    {
        Task<Role> InsertAsync(Role author);

        Task<Role> GetAuthorByNameAsync(string name);

        Task<IEnumerable<Role>> GetAllAcync();

        void Insert(Role author);

        void Delete(Role author);

        void Update(Role author);

        IEnumerable<Role> Filter(Expression<Func<Role, bool>> filter);

        IEnumerable<Role> GetAll();

        Role Get(int id);

        bool IsExist(Expression<Func<Role, bool>> filter);

        Role GetByName(string name);
    }
}
