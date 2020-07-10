using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQlLibary.BLL.Services
{
    public class PostSrvice : IPostService
    {
        private readonly IPostRepository _postRepository;

        private readonly IUnitOfWork _unitOfWork;

        public PostSrvice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _postRepository = unitOfWork.PostRepository;
        }

        public void Delete(Post post)
        {
            _postRepository.Delete(post);
            _unitOfWork.Commit();
        }

        public IEnumerable<Post> Filter(Expression<Func<Post, bool>> filter)
        {
            return _postRepository.Filter(filter);
        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public void Insert(Post post)
        {
            _postRepository.Insert(post);
            _unitOfWork.Commit();
        }

        public bool IsExist(Expression<Func<Post, bool>> filter)
        {
            return _postRepository.IsExist(filter);
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
            _unitOfWork.Commit();
        }

        public Post GetByLikes(int likes)
        {
            return _postRepository.GetByLikes(likes);
        }
    }
}
