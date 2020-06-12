using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Name = "Author";
            Description = "This is author model persistant info about author.";
            Field(d => d.Name, nullable: true).Description("The name of the author.");
            Field(x => x.Id);
            Field<ListGraphType<BookType>>("books", "Which books they appear in.");//IDataLoaderContextAccessor accessor
        }
    }
}
