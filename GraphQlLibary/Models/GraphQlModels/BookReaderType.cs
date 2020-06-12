using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class BookReaderType : ObjectGraphType<BookReader>
    {
        public BookReaderType()
        {
            Field(x => x.BookId);
            Field(x => x.ReaderId);
            Field<BookType, Book>().Name("Book").Resolve(x => x.Source.Book);
            Field<ReaderType, Reader>().Name("Reader").Resolve(x => x.Source.Reader);
        }
    }
}
