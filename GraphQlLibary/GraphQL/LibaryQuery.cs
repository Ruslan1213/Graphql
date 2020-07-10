using System.Collections.Generic;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Models.GraphQlModels;

namespace GraphQlLibary.GraphQL
{
    public class LibaryQuery : ObjectGraphType
    {
        public LibaryQuery(IDataLoaderContextAccessor accessor, IRoleService authorService, ICommentService commentService, IPostService postService, IUserService userService)
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
                    var barcode = ctx.GetArgument<int>("id");
                    return postService.Get(barcode);
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
