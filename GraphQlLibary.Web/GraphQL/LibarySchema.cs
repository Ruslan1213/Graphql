using GraphQL;
using GraphQL.Types;
using GraphQL.Upload.AspNetCore;

namespace GraphQlLibary.Web.GraphQL
{
    public class LibarySchema : Schema
    {
        public LibarySchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<LibaryQuery>();
            Mutation = resolver.Resolve<LibaryMutation>();
            RegisterValueConverter(new FormFileConverter());
        }
    }
}
