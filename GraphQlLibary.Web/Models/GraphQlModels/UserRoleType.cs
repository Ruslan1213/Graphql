using GraphQL.Types;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Models.GraphQlModels.RoleModels;
using GraphQlLibary.Web.Models.GraphQlModels.UserModels;

namespace GraphQlLibary.Web.Models.GraphQlModels
{
    public class UserRoleType : ObjectGraphType<UserRole>
    {
        public UserRoleType()
        {
            Field(x => x.RoleId);
            Field(x => x.UserId);
            Field<RoleType, Role>().Name("role").Resolve(x => x.Source.Role);
            Field<UserType, User>().Name("user").Resolve(x => x.Source.User);
        }
    }
}
