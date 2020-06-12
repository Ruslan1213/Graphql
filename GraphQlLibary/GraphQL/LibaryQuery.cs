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
        public LibaryQuery(IDataLoaderContextAccessor accessor, IAuthorService authorService)
        {

            Field<ListGraphType<AuthorType>, IEnumerable<Author>>()
                .Name("AuthorItems")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllAuthors", () => authorService.GetAllAcync());
                    return loader.LoadAsync();
                });

            Field<AuthorType, Author>()
                .Name("Author")
                .Argument<NonNullGraphType<StringGraphType>>("name", "Author name")
                .ResolveAsync(ctx =>
                {
                    var barcode = ctx.GetArgument<string>("name");
                    return authorService.GetAuthorByNameAsync(barcode);
                });
        }
    }
}
