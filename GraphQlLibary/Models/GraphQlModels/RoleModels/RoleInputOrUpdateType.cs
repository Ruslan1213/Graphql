using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class RoleInputOrUpdateType : InputObjectGraphType<Role>
    {
        public RoleInputOrUpdateType()
        {
            Name = "RoleInput";
            Field<NonNullGraphType<StringGraphType>>("Name");
        }
    }
}
