using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using GraphQL.Upload.AspNetCore;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Interfaces;
using GraphQlLibary.Web.Models.GraphQlModels.CommentModel;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;
using GraphQlLibary.Web.Models.GraphQlModels.RoleModels;
using GraphQlLibary.Web.Models.GraphQlModels.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace GraphQlLibary.Web.GraphQL
{
    public class LibaryQuery : ObjectGraphType
    {
        public LibaryQuery(
            IDataLoaderContextAccessor accessor,
            IRoleService authorService,
            ICommentService commentService,
            IPostService postService,
            IUserService userService,
            IHttpContextAccessor contextAccessor,
            ITokenService tokenService)
        {
            Field<ListGraphType<RoleType>, IEnumerable<Role>>()
                .Name("RoleItems")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllRoles", () => authorService.GetAllAcync());

                    if (tokenService.IsAdmin(token: GetToken(contextAccessor)))
                    {
                        return loader.LoadAsync();
                    }

                    throw new ExecutionError("401");
                });

            Field<RoleType, Role>()
                .Name("Role")
                .Argument<NonNullGraphType<StringGraphType>>("id", "Role Id")
                .Resolve(ctx =>
                {
                    var barcode = ctx.GetArgument<int>("id");
                    return authorService.Get(barcode);
                });


            Field<ListGraphType<CommentType>, IEnumerable<Comment>>()
                .Name("CommentItems")
                .Resolve(ctx =>
                {
                    var loader = commentService.GetAll();
                    return loader;
                });

            Field<CommentType, Comment>()
                .Name("Comment")
                .Argument<NonNullGraphType<StringGraphType>>("id", "Comment Id")
                .Resolve(ctx =>
                {
                    var barcode = ctx.GetArgument<int>("id");
                    return commentService.Get(barcode);
                });

            Field<StringGraphType>("singleUpload",
                arguments: new QueryArguments(
                    new QueryArgument<UploadGraphType> { Name = "file" }),
                    resolve: context =>
                    {
                        var file = context.GetArgument<IFormFile>("file");
                        return file.FileName;
                    });

            Field<ListGraphType<PostType>, IEnumerable<Post>>()
                .Name("PostItems")
                .Resolve(ctx =>
                {
                    return postService.GetAll();
                });

            Field<PostType, Post>()
                .Name("Post")
                .Argument<NonNullGraphType<StringGraphType>>("id", "RPostole Id")
                .Resolve(ctx =>
                {
                    try
                    {
                        var barcode = ctx.GetArgument<int>("id");

                        return postService.Get(barcode);
                    }
                    catch
                    {
                        return null;
                    }

                });

            Field<BooleanGraphType, bool>()
                .Name("IsAdmin")
                .Resolve(ctx =>
                {
                    var isAdmin = tokenService.IsAdmin(GetToken(contextAccessor));

                    return isAdmin;
                });

            Field<ListGraphType<UserType>, IEnumerable<User>>()
                .Name("UserItems")
                .Resolve(ctx =>
                {
                    return userService.GetAll();
                });

            Field<UserType, User>()
                .Name("User")
                .Argument<NonNullGraphType<StringGraphType>>("id", "User Id")
                .Resolve(ctx =>
                {
                    var barcode = ctx.GetArgument<int>("id");
                    return userService.Get(barcode);
                });
        }

        private string GetToken(IHttpContextAccessor contextAccessor)
        {
            StringValues token;
            contextAccessor.HttpContext.Request.Headers.TryGetValue("token", out token);

            return token.ToString();
        }
    }
}
