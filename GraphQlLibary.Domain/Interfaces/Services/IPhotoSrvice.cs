using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GraphQlLibary.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace GraphQlLibary.Domain.Interfaces.Services
{
    public interface IPhotoService
    {
        void Insert(Photo comment);

        void Delete(Photo comment);

        void Update(Photo comment);

        IEnumerable<Photo> Filter(Expression<Func<Photo, bool>> filter);

        IEnumerable<Photo> GetAll();

        Photo Get(int id);

        bool IsExist(Expression<Func<Photo, bool>> filter);

        string CreatePhoto(IFormFile file, Post post);

        void DeletePhoto(Post post);
    }
}
