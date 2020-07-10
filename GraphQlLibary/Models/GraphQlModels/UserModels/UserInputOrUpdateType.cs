using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class UserInputOrUpdateType : InputObjectGraphType<User>
    {
        public UserInputOrUpdateType()
        {
            Name = "UserInput";
            Field<NonNullGraphType<StringGraphType>>("Name");
            Field<NonNullGraphType<StringGraphType>>("Password");
            Field<NonNullGraphType<StringGraphType>>("Email");
            Field<ListGraphType<IntGraphType>>("UserRole"); 
        }
    }
}
