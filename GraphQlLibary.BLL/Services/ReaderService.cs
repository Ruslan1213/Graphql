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
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ReaderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readerRepository = unitOfWork.ReaderRepository;
        }

        public void Delete(Reader reader)
        {
            if (reader == null)
            {
                return;
            }

            _readerRepository.Delete(reader);
            _unitOfWork.Commit();
        }

        public IEnumerable<Reader> Filter(Expression<Func<Reader, bool>> filter)
        {
            return _readerRepository.Filter(filter);
        }

        public Reader Get(string id)
        {
            return _readerRepository.Get(id);
        }

        public IEnumerable<Reader> GetAll()
        {
            return _readerRepository.GetAll();
        }

        public Reader GetByMail(string mail)
        {
            return _readerRepository.GetByMail(mail);
        }

        public Reader GetByMailAndPassword(string mail, string password)
        {
            return _readerRepository.GetByMailAndPassword(mail, password.GetHashString());
        }

        public void Insert(Reader reader)
        {
            if (IsExist(x => x.Email == reader.Email))
            {
                return;
            }

            reader.Password = reader.Password.GetHashString();
            _readerRepository.Insert(reader);
            _unitOfWork.Commit();
        }

        public bool IsExist(Expression<Func<Reader, bool>> filter)
        {
            return _readerRepository.IsExist(filter);
        }

        public void Update(Reader reader)
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
