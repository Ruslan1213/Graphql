using GraphQL.Types;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;
using GraphQlLibary.Web.Models.GraphQlModels.UserModels;

namespace GraphQlLibary.Web.Models.GraphQlModels.Likes
{
    public class LikeType : ObjectGraphType<Like>
    {
        public LikeType()
        {
            Field(x => x.PostId);
            Field(x => x.UserId);
            Field<PostType>("post", "Which posts they appear in.");
            Field<UserType>("user", "Which posts they appear in.");
        }
    }
}
