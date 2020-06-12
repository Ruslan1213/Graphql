using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlLibary.DAL.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly LibaryContext _libaryContext;

        public AuthorRepository(LibaryContext libaryContext) : base(libaryContext)
        {
            _libaryContext = libaryContext;
        }

        public async Task<IEnumerable<Author>> GetAllAcync()
        {
            return await _libaryContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await _libaryContext.Authors.FirstOrDefaultAsync(x => x.Name == name);
        }

        public Author GetByName(string name)
        {
            return _libaryContext.Authors.FirstOrDefault(x => x.Name == name);
        }
    }
}
