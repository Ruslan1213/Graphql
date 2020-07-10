using GraphQL;
using GraphQL.Types;
using GraphQL.Upload.AspNetCore;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Auth;
using GraphQlLibary.Web.Models.GraphQlModels.Auth;
using GraphQlLibary.Web.Models.GraphQlModels.CommentModel;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;
using GraphQlLibary.Web.Models.GraphQlModels.RoleModels;
using GraphQlLibary.Web.Models.GraphQlModels.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace GraphQlLibary.Web.GraphQL
{
    public class LibaryMutation : ObjectGraphType
    {
        private readonly IRoleService _roleService;

        public LibaryMutation(IRoleService roleService, IUserService userService, IPostService postService, ICommentService commentService, IHttpContextAccessor contextAccessor)
        {
            #region Authorize
            _roleService = roleService;

            Field<SessionType, Session>()
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
                        return new Session { IsLoggedIn = false };
                    }

                    var claims = GenerateClaims(user);
                    var principal = CreatePrincipal(claims);

                    contextAccessor.HttpContext.SignInAsync(principal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMonths(6),
                        IsPersistent = true
                    });

                    return new Session { IsLoggedIn = true };
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

        #region privateMethods
        private ClaimsPrincipal CreatePrincipal(IEnumerable<Claim> claims)
        {
            var claimsIdentityList = new List<ClaimsIdentity> {
                new ClaimsIdentity(claims),
                new ClaimsIdentity("Cookie")
            };

            return new ClaimsPrincipal(claimsIdentityList);
        }

        private IEnumerable<Claim> GenerateClaims(User user)
        {
            //var a = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var claims = new List<Claim>
                             {
                                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                 new Claim(ClaimTypes.Name, user.Name),
                                 new Claim(ClaimTypes.Email, user.Email)
                             };

            var roles = user.UserRole.Select(x => _roleService.Get(x.RoleId)).ToList();

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return claims;
        }
        #endregion privateMethods
    }
}
