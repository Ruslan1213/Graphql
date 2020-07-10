using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Web.Models.GraphQlModels.CommentModel
{
    public class CommentInputOrUpdateType : InputObjectGraphType<Comment>
    {
        public CommentInputOrUpdateType()
        {
            Name = "UserInput";
            Field<NonNullGraphType<IntGraphType>>("ParentCommentId");
            Field<NonNullGraphType<StringGraphType>>("Text");
            Field<NonNullGraphType<StringGraphType>>("CommentatorName");
            Field<NonNullGraphType<IntGraphType>>("PostId");
            Field<NonNullGraphType<IntGraphType>>("Id");

        }
    }
}