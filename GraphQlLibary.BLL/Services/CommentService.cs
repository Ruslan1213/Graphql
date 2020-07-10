using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = _unitOfWork.CommentRepository;
        }

        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
            _unitOfWork.Commit();
        }

        public IEnumerable<Comment> Filter(Expression<Func<Comment, bool>> filter)
        {
            return _commentRepository.Filter(filter);
        }

        public Comment Get(int id)
        {
            return _commentRepository.Get(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public void Insert(Comment comment)
        {
            _commentRepository.Insert(comment);
        }

        public bool IsExist(Expression<Func<Comment, bool>> filter)
        {
            return _commentRepository.IsExist(filter);
        }

        public void Update(Comment comment)
        {
            _commentRepository.Update(comment);
        }
    }
}
