using GraphQlLibary.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQlLibary.Domain.Interfaces.Repository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetByName(string name);

        Task<IEnumerable<Author>> GetAllAcync();

        Task<Author> GetAuthorByNameAsync(string name);
    }
}
