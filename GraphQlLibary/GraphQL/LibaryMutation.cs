using GraphQL.Types;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Models.GraphQlModels;


namespace GraphQlLibary.GraphQL
{
    public class LibaryMutation : ObjectGraphType
    {
        public LibaryMutation(IRoleService roleService, IUserService userService, IPostService postService, ICommentService commentService)
        {
            #region RoleOperation

            Field<RoleType, Role>()
                .Name("createRole")
                .Argument<NonNullGraphType<RoleInputOrUpdateType>>("role", "role input")
                .ResolveAsync(ctx =>
                {
                    var item = ctx.GetArgument<Role>("role");
                    roleService.Insert(item);

                    return new System.Threading.Tasks.Task<Role>(null, null);
                });

            Field<RoleType, Role>()
                .Name("updateRole")
                .Argument<NonNullGraphType<RoleInputOrUpdateType>>("role", "role update")
                .Resolve(ctx =>
                {
                    var item = ctx.GetArgument<Role>("role");
                    roleService.Update(item);

                    return item;
                });

            Field<RoleType, Role>()
               .Name("deleteRole")
               .Argument<NonNullGraphType<RoleInputOrUpdateType>>("role", "role to delete")
               .Resolve(ctx =>
               {
                   var item = ctx.GetArgument<Role>("role");

                   if (roleService.IsExist(x => x.Id == item.Id))
                   {
                       roleService.Delete(item);
                   }

                   return item;
               });

            #endregion RoleOperation

            #region UserOperation

            Field<UserType, User>()
                .Name("createUser")
                .Argument<NonNullGraphType<UserInputOrUpdateType>>("user", "user input")
                .Resolve(ctx =>
                {
                    var item = ctx.GetArgument<User>("user");
                    userService.Insert(item);

                    return item;
                });

            Field<UserType, User>()
                .Name("updateUser")
                .Argument<NonNullGraphType<UserInputOrUpdateType>>("user", "user update")
                .Resolve(ctx =>
                {
                    var item = ctx.GetArgument<User>("user");
                    userService.Update(item);

                    return item;
                });

            Field<UserType, User>()
               .Name("deleteUser")
               .Argument<NonNullGraphType<UserInputOrUpdateType>>("user", "role to delete")
               .Resolve(ctx =>
               {
                   var item = ctx.GetArgument<User>("user");

                   if (userService.IsExist(x => x.Id == item.Id))
                   {
                       userService.Delete(item);
                   }

                   return item;
               });

            #endregion UserOperation

            #region PostOperation

            Field<PostType, Post>()
              .Name("createPost")
              .Argument<NonNullGraphType<PostInputOrUpdateType>>("post", "post input")
              .Resolve(ctx =>
              {
                  var item = ctx.GetArgument<Post>("post");
                  postService.Insert(item);

                  return item;
              });

            Field<PostType, Post>()
                .Name("updatePost")
                .Argument<NonNullGraphType<PostInputOrUpdateType>>("post", "user update")
                .Resolve(ctx =>
                {
                    var item = ctx.GetArgument<Post>("post");
                    postService.Update(item);

                    return item;
                });

            Field<PostType, Post>()
               .Name("deletePost")
               .Argument<NonNullGraphType<PostInputOrUpdateType>>("Post", "Post to delete")
               .Resolve(ctx =>
               {
                   var item = ctx.GetArgument<Post>("post");

                   if (postService.IsExist(x => x.Id == item.Id))
                   {
                       postService.Delete(item);
                   }

                   return item;
               });

            #endregion PostOperation

            #region CommentOperation

            Field<CommentType, Comment>()
             .Name("createComment")
             .Argument<NonNullGraphType<CommentInputOrUpdateType>>("comment", "Comment input")
             .Resolve(ctx =>
             {
                 var item = ctx.GetArgument<Comment>("comment");
                 commentService.Insert(item);

                 return item;
             });

            Field<CommentType, Comment>()
                .Name("updateComment")
                .Argument<NonNullGraphType<CommentInputOrUpdateType>>("Comment", "Comment update")
                .Resolve(ctx =>
                {
                    var item = ctx.GetArgument<Comment>("comment");
                    commentService.Update(item);

                    return item;
                });

            Field<CommentType, Comment>()
               .Name("deleteComment")
               .Argument<NonNullGraphType<CommentInputOrUpdateType>>("Comment", "Comment to delete")
               .Resolve(ctx =>
               {
                   var item = ctx.GetArgument<Comment>("comment");

                   if (commentService.IsExist(x => x.Id == item.Id))
                   {
                       commentService.Delete(item);
                   }

                   return item;
               });

            #endregion CommentOperation
        }
    }
}
