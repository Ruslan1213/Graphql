using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class ReaderType : ObjectGraphType<Reader>
    {
        public ReaderType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Password);
            Field(x => x.Email);
            Field<ListGraphType<BookReaderType>>("BookReader", "Which books they appear in.");
        }
    }
}
