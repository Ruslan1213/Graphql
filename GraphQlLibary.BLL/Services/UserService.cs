using GraphQlLibary.DAL.Extensions;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _readerRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readerRepository = unitOfWork.ReaderRepository;
        }

        public void Delete(User reader)
        {
            if (reader == null)
            {
                return;
            }

            _readerRepository.Delete(reader);
            _unitOfWork.Commit();
        }

        public IEnumerable<User> Filter(Expression<Func<User, bool>> filter)
        {
            return _readerRepository.Filter(filter);
        }

        public User Get(int id)
        {
            return _readerRepository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _readerRepository.GetAll();
        }

        public User GetByMail(string mail)
        {
            return _readerRepository.GetByMail(mail);
        }

        public User GetByMailAndPassword(string mail, string password)
        {
            return _readerRepository.GetByMailAndPassword(mail, password.GetHashString());
        }

        public void Insert(User reader)
        {
            if (IsExist(x => x.Email == reader.Email))
            {
                return;
            }

            reader.Password = reader.Password.GetHashString();
            _readerRepository.Insert(reader);
            _unitOfWork.Commit();
        }

        public bool IsExist(Expression<Func<User, bool>> filter)
        {
            return _readerRepository.IsExist(filter);
        }

        public void Update(User reader)
        {
            if (IsExist(x => x.Email == reader.Email && x.Id != reader.Id))
            {
                return;
            }

            _readerRepository.Update(reader);
            _unitOfWork.Commit();
        }
    }
}
