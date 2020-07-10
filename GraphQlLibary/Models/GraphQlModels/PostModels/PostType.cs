using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
{
    public class PostType : ObjectGraphType<Post>
    {
        public PostType()
        {
            Field(x => x.Id);
            Field(x => x.DateOfPost);
            Field(x => x.Likes);
            Field(x => x.UserId);
            Field(x => x.Description);
            Field<UserType, User>().Name("user").Resolve(x => x.Source.User);
            Field<ListGraphType<CommentType>>("comments", "Which comments they appear in."); ;
            Field<ListGraphType<PhotoType>>();
        }
    }
}
