using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlLibary.DAL.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly LibaryContext _libaryContext;

        public RoleRepository(LibaryContext libaryContext) : base(libaryContext)
        {
            _libaryContext = libaryContext;
        }

        public async Task<IEnumerable<Role>> GetAllAcync()
        {
            return await _libaryContext.Roles.ToListAsync();
        }

        public async Task<Role> GetAuthorByNameAsync(string name)
        {
            return await _libaryContext.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }

        public Role GetByName(string name)
        {
            return _libaryContext.Roles.FirstOrDefault(x => x.Name == name);
        }
    }
}
