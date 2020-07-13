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
using System.Collections.Generic;

namespace GraphQlLibary.Web.GraphQL
{
    public class LibaryMutation : ObjectGraphType
    {
        public LibaryMutation(ITokenService tokenService, IRoleService roleService, IUserService userService, IPostService postService, ICommentService commentService, IHttpContextAccessor contextAccessor)
        {
            #region AddLike
            Field<BooleanGraphType, bool>()
               .Name("likePost")
               .Argument<NonNullGraphType<IntGraphType>>("postId", "postId input")
               .Argument<NonNullGraphType<IntGraphType>>("userId", "userId input")
               .Resolve(context =>
               {
                   int userId = context.GetArgument<int>("userId");
                   int postId = context.GetArgument<int>("userId");

                   return postService.AddLike(new Like { UserId = userId, PostId = postId }); ;
               });
            #endregion AddLike

            #region Authorize

            Field<StringGraphType, string>()
                .Name("sessions")
                .Argument<NonNullGraphType<StringGraphType>>("password", "password input")
                .Argument<NonNullGraphType<StringGraphType>>("mail", "mail input")
                .Resolve(context =>
                {
                    string password = context.GetArgument<string>("password");
                    string mail = context.GetArgument<string>("mail");
                    var user = userService.GetByMailAndPassword(mail, password);

                    if (user == null)
                    {
                        return null;
                    }

                    return tokenService.GenerateJwtToken(user);
                });
            #endregion Authorize

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
                .Name("registration")
                .Argument<NonNullGraphType<RegisterUserType>>("user", "user input")
                .Resolve(ctx =>
                {
                    var item = ctx.GetArgument<User>("user");
                    item.UserRole = new List<UserRole> { new UserRole { RoleId = 1 } };
                    userService.Insert(item);

                    return item;
                });

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

            Field<StringGraphType>("singleUpload",
                arguments: new QueryArguments(
                new QueryArgument<UploadGraphType> { Name = "file" }),
                resolve: context =>
                {
                    var file = context.GetArgument<IFormFile>("file");
                    return file.FileName;
                });

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
               .Argument<NonNullGraphType<PostToDeleteType>>("Post", "Post to delete")
               .Resolve(ctx =>
               {
                   var item = ctx.GetArgument<Post>("post");

                   if (postService.IsExist(x => x.Id == item.Id))
                   {
                       postService.Delete(postService.Get(item.Id));

                       return item;
                   }

                   return null;
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
