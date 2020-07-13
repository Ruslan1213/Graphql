using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Web.Models.GraphQlModels.UserModels
{
    public class RegisterUserType : InputObjectGraphType<User>
    {
        public RegisterUserType()
        {
            Name = "RegisterInput";
            Field<NonNullGraphType<StringGraphType>>("Name");
            Field<NonNullGraphType<StringGraphType>>("Password");
            Field<NonNullGraphType<StringGraphType>>("Email");
        }
    }
}
