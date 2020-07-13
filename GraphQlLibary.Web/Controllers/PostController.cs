using System;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace GraphQlLibary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ITokenService _tokenService;
        private readonly IPhotoService _photoService;

        public PostController(IPostService postService, ITokenService tokenService, IPhotoService photoService)
        {
            _postService = postService;
            _tokenService = tokenService;
            _photoService = photoService;
        }


        [HttpPost("createPost")]
        public bool CreatePost([FromForm] string description, IFormFile file)
        {
            if (string.IsNullOrEmpty(description))
            {
                return false;
            }

            try
            {
                Post post = new Post();
                post.DateOfPost = DateTime.UtcNow;
                post.Description = description;
                post.UserId = _tokenService.GetId(GetToken());

                if (post.UserId == 0 || file == null)
                {
                    return false;
                }

                post.PhotoUri = _photoService.CreatePhoto(file, post);
                _postService.Insert(post);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetToken()
        {
            StringValues token;
            HttpContext.Request.Headers.TryGetValue("token", out token);

            return token.ToString();
        }
    }
}
