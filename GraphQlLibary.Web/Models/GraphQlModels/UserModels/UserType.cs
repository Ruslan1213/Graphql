using GraphQL.Types;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;

namespace GraphQlLibary.Web.Models.GraphQlModels.UserModels
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Password);
            Field(x => x.Email);
            Field<ListGraphType<PostType>>("posts", "Which comments they appear in.");
            Field<ListGraphType<UserRoleType>>("userRole", "Which userRoles they appear in.");
        }
    }
}
