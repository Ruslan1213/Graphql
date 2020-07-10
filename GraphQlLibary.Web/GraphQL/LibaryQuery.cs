using System.Collections.Generic;
using System.Security.Claims;
using GraphQL.DataLoader;
using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using GraphQL.Upload.AspNetCore;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Models.GraphQlModels.CommentModel;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;
using GraphQlLibary.Web.Models.GraphQlModels.RoleModels;
using GraphQlLibary.Web.Models.GraphQlModels.UserModels;
using Microsoft.AspNetCore.Http;

namespace GraphQlLibary.Web.GraphQL
{
    public class LibaryQuery : ObjectGraphType
    {
        private readonly IRoleService _roleService;

        public LibaryQuery(
            IDataLoaderContextAccessor accessor,
            IRoleService authorService,
            ICommentService commentService,
            IPostService postService,
            IUserService userService,
            IHttpContextAccessor contextAccessor)
        {
            Field<ListGraphType<RoleType>, IEnumerable<Role>>()
                .Name("RoleItems")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllRoles", () => authorService.GetAllAcync());

                    return loader.LoadAsync();
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
                    catch (System.FormatException e)
                    {
                        return null;
                    }

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
    }
}
