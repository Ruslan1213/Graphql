using Autofac;
using GraphQL.Types;
using GraphQL;
using GraphQL.Http;
using GraphQL.DataLoader;
using GraphQlLibary.DAL.Repositories;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.DAL.UnitOfWork;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.BLL.Services;
using GraphQlLibary.DAL.Context;
using GraphQlLibary.Web.GraphQL;
using GraphQlLibary.Web.Models.GraphQlModels.CommentModel;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;
using GraphQlLibary.Web.Models.GraphQlModels.RoleModels;
using GraphQlLibary.Web.Models.GraphQlModels.UserModels;
using GraphQlLibary.Web.Models.GraphQlModels;
using GraphQlLibary.Web.Models.GraphQlModels.Auth;

namespace GraphQlLibary.Web.Infrastructure.DI
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<Repository<Comment>>().As<IRepository<Comment>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Photo>>().As<IRepository<Photo>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Post>>().As<IRepository<Post>>().InstancePerLifetimeScope();

            builder.RegisterType<Repository<Role>>().As<IRepository<Role>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Role>>().As<IRepository<Role>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<User>>().As<IRepository<User>>().InstancePerLifetimeScope();

            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<PostSrvice>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<PhotoService>().As<IPhotoService>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<LibaryContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<LibarySchema>().As<ISchema>().SingleInstance();
            builder.RegisterType<DocumentWriter>().As<IDocumentWriter>().SingleInstance();
            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>().SingleInstance();
            builder.RegisterType<DataLoaderContextAccessor>().As<IDataLoaderContextAccessor>().SingleInstance();

            builder.RegisterType<DataLoaderDocumentListener>().AsSelf().SingleInstance();
            builder.RegisterType<LibaryQuery>().AsSelf().SingleInstance();
            builder.RegisterType<LibaryMutation>().AsSelf().SingleInstance();

            builder.RegisterType<CommentType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoleType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserType>().AsSelf().InstancePerLifetimeScope();
            
            builder.RegisterType<PostToDeleteType>().AsSelf().SingleInstance();
            builder.RegisterType<RoleInputOrUpdateType>().AsSelf().SingleInstance();
            builder.RegisterType<CommentInputOrUpdateType>().AsSelf().SingleInstance();
            builder.RegisterType<PostInputOrUpdateType>().AsSelf().SingleInstance();
            builder.RegisterType<UserInputOrUpdateType>().AsSelf().SingleInstance();

            builder.RegisterType<PostType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SessionType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PhotoType>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
