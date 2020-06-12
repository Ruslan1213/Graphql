using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class AuthorInputOrUpdateType : InputObjectGraphType<Author>
    {
        public AuthorInputOrUpdateType()
        {
            Name = "AuthorInput";
            Field<NonNullGraphType<StringGraphType>>("Name");
        }
    }
}
