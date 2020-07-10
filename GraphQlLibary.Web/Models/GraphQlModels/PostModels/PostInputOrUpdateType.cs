using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Web.Models.GraphQlModels.PostModels
{
    public class PostInputOrUpdateType : InputObjectGraphType<Post>
    {
        public PostInputOrUpdateType()
        {
            Name = "UserInput";
            Field<NonNullGraphType<IntGraphType>>("Id");
            Field<NonNullGraphType<DateTimeGraphType>>("DateOfPost");
            Field<NonNullGraphType<StringGraphType>>("Description");
            Field<ListGraphType<IntGraphType>>("Likes");
            Field<ListGraphType<IntGraphType>>("UserId");
        }
    }
}
