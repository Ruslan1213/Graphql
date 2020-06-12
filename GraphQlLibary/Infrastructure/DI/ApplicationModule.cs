using Autofac;
using GraphQL.Types;
using GraphQL;
using GraphQL.Http;
using GraphQL.DataLoader;
using GraphQlLibary.Models.GraphQlModels;
using GraphQlLibary.GraphQL;
using GraphQlLibary.DAL.Repositories;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.DAL.UnitOfWork;
using GraphQlLibary.Domain.Interfaces.UnitOfWork;
using GraphQlLibary.BLL.Services;
using GraphQlLibary.DAL.Context;

namespace GraphQlLibary.Infrastructure.DI
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ReaderRepository>().As<IReaderRepository>().InstancePerLifetimeScope();

            builder.RegisterType<Repository<Author>>().As<IRepository<Author>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Book>>().As<IRepository<Book>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Reader>>().As<IRepository<Reader>>().InstancePerLifetimeScope();

            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
            builder.RegisterType<ReaderService>().As<IReaderService>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<LibaryContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<LibarySchema>().As<ISchema>().SingleInstance();
            builder.RegisterType<DocumentWriter>().As<IDocumentWriter>().SingleInstance();
            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>().SingleInstance();
            builder.RegisterType<DataLoaderContextAccessor>().As<IDataLoaderContextAccessor>().SingleInstance();

            builder.RegisterType<DataLoaderDocumentListener>().AsSelf().SingleInstance();
            builder.RegisterType<LibaryQuery>().AsSelf().SingleInstance();
            builder.RegisterType<LibaryMutation>().AsSelf().SingleInstance();

            builder.RegisterType<AuthorType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookReaderType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ReaderType>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AuthorInputOrUpdateType>().AsSelf().SingleInstance();
        }
    }
}
