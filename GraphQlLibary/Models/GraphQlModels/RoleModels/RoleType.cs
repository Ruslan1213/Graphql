using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class RoleType : ObjectGraphType<Role>
    {
        public RoleType()
        {
            Field(x => x.Id);
            Field(d => d.Name, nullable: true).Description("The name of the book.");
            Field<ListGraphType<UserRoleType>>("userRole", "Which role they appear in.");
        }
    }
}
