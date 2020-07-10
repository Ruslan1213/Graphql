using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using GraphQlLibary.Models.ConfigModels;
using Microsoft.Extensions.Options;

namespace GraphQlLibary.BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IRepository<Photo> _photoRepository;

        private readonly IUnitOfWork _unitOfWork;

        private Cloudinary _cloudinary;

        public PhotoService(IUnitOfWork unitOfWork, IOptions<CloudDictionaryConfig> cloudinaryConfig)
        {
            _unitOfWork = unitOfWork;
            _photoRepository = _unitOfWork.PhotoRepository;

            Account account = new Account(
             cloudinaryConfig.Value.CloudName,
             cloudinaryConfig.Value.ApiKey,
             cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        public void Delete(Photo photo)
        {
        }

        public IEnumerable<Photo> Filter(Expression<Func<Photo, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Photo Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photo> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Photo comment)
        {
        }

        public bool IsExist(Expression<Func<Photo, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Photo comment)
        {
        }

        public string CreatePhoto(IFormFile file, Post post)
        {
            var fileLength = file == null ? 0 : file.Length;

            if (fileLength > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                                               .Width(500).Height(500)
                                               .Crop("fill")
                                               .Gravity("face"),
                        Tags = post.Description
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);

                    return uploadResult.Uri?.ToString();
                }
            }

            return null;
        }

        public void DeletePhoto(Post participant)
        {
            _cloudinary.DeleteResourcesByTag(participant.Description);
        }
    }
}
