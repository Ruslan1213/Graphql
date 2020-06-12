using GraphQL;
using GraphQL.Types;

namespace GraphQlLibary.GraphQL
{
    public class LibarySchema : Schema
    {
        public LibarySchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<LibaryQuery>();
            Mutation = resolver.Resolve<LibaryMutation>();
        }
    }
}
