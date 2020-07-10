using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GraphQlLibary.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _authorRepository;

        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = unitOfWork.AuthorRepository;
        }

        public void Delete(Role author)
        {
            _authorRepository.Delete(author);
            _unitOfWork.Commit();
        }

        public IEnumerable<Role> Filter(Expression<Func<Role, bool>> filter)
        {
            return _authorRepository.Filter(filter);
        }

        public Role Get(int id)
        {
            return _authorRepository.Get(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public async Task<IEnumerable<Role>> GetAllAcync()
        {
            return await _authorRepository.GetAllAcync();
        }

        public async Task<Role> GetAuthorByNameAsync(string name)
        {
            return await _authorRepository.GetAuthorByNameAsync(name);
        }

        public Role GetByName(string name)
        {
            return _authorRepository.GetByName(name);
        }

        public void Insert(Role author)
        {
            if (IsExist(x => x.Name == author.Name))
            {
                return;
            }

            _authorRepository.Insert(author);
            _unitOfWork.Commit();
        }

        public Task<Role> InsertAsync(Role author)
        {
            _authorRepository.Insert(author);
            _unitOfWork.Commit();

            return Task.FromResult(author);
        }

        public bool IsExist(Expression<Func<Role, bool>> filter)
        {
            return _authorRepository.IsExist(filter);
        }

        public void Update(Role author)
        {
            if (IsExist(x => x.Name == author.Name && x.Id != author.Id))
            {
                return;
            }

            _authorRepository.Update(author);
            _unitOfWork.Commit();
        }
    }
}
