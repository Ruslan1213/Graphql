using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Field(d => d.Name, nullable: true).Description("The name of the book.");
            Field(d => d.Descriprtion, nullable: true).Description("The description of the book.");
            Field(x => x.Id);
            Field(x => x.AuthorId);
            Field<AuthorType, Author>().Name("Author").Resolve(x => x.Source.Author);
            Field<ListGraphType<BookReaderType>>("BookReader", "Which books they appear in.");
        }
    }
}
