using GraphQlLibary.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQlLibary.Domain.Interfaces.Repository
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);

        Task<IEnumerable<Role>> GetAllAcync();

        Task<Role> GetAuthorByNameAsync(string name);
    }
}
