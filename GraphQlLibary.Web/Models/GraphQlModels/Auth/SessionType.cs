using GraphQL.Types;
using GraphQlLibary.Web.Auth;

namespace GraphQlLibary.Web.Models.GraphQlModels.Auth
{
    public class SessionType : ObjectGraphType<Session>
    {
        public SessionType()
        {
            Field(t => t.IsLoggedIn);
        }
    }
}
