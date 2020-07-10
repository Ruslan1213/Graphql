using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Web.Models.GraphQlModels.RoleModels
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
