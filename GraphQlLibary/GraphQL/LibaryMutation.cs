using GraphQL.Types;
using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Models.GraphQlModels;


namespace GraphQlLibary.GraphQL
{
    public class LibaryMutation : ObjectGraphType
    {
        public LibaryMutation(IAuthorService authorService)
        {
            Field<AuthorType, Author>()
                .Name("createAuthor")
                .Argument<NonNullGraphType<AuthorInputOrUpdateType>>("author", "author input")
                .ResolveAsync(ctx =>
                {
                    var item = ctx.GetArgument<Author>("author");

                    return authorService.InsertAsync(item);
                });

        }
    }
}
