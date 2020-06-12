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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = unitOfWork.AuthorRepository;
        }

        public void Delete(Author author)
        {
            _authorRepository.Delete(author);
            _unitOfWork.Commit();
        }

        public IEnumerable<Author> Filter(Expression<Func<Author, bool>> filter)
        {
            return _authorRepository.Filter(filter);
        }

        public Author Get(string id)
        {
            return _authorRepository.Get(id);
        }

        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public async Task<IEnumerable<Author>> GetAllAcync()
        {
            return await _authorRepository.GetAllAcync();
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await _authorRepository.GetAuthorByNameAsync(name);
        }

        public Author GetByName(string name)
        {
            return _authorRepository.GetByName(name);
        }

        public void Insert(Author author)
        {
            if (IsExist(x => x.Name == author.Name))
            {
                return;
            }

            _authorRepository.Insert(author);
            _unitOfWork.Commit();
        }

        public Task<Author> InsertAsync(Author author)
        {
            _authorRepository.Insert(author);
            _unitOfWork.Commit();

            return Task.FromResult(author);
        }

        public bool IsExist(Expression<Func<Author, bool>> filter)
        {
            return _authorRepository.IsExist(filter);
        }

        public void Update(Author author)
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
