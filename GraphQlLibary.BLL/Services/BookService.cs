using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = unitOfWork.BookRepository;
        }

        public void Delete(Book book)
        {
            _bookRepository.Delete(book);
            _unitOfWork.Commit();
        }

        public IEnumerable<Book> Filter(Expression<Func<Book, bool>> filter)
        {
            return _bookRepository.Filter(filter);
        }

        public Book Get(string id)
        {
            return _bookRepository.Get(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book GetByDescription(string description)
        {
            return _bookRepository.GetByDescription(description);
        }

        public void Insert(Book book)
        {
            if (IsExist(x => x.Descriprtion == book.Descriprtion))
            {
                return;
            }

            _bookRepository.Insert(book);
            _unitOfWork.Commit();
        }

        public bool IsExist(Expression<Func<Book, bool>> filter)
        {
            return _bookRepository.IsExist(filter);
        }

        public void Update(Book book)
        {
            if (IsExist(x => x.Descriprtion == book.Descriprtion && x.Id != book.Id))
            {
                return;
            }

            _bookRepository.Update(book);
            _unitOfWork.Commit();
        }
    }
}
