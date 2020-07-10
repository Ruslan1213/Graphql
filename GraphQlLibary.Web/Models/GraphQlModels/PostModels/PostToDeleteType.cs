using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Web.Models.GraphQlModels.PostModels
{
    public class PostToDeleteType : InputObjectGraphType<Post>
    {
        public PostToDeleteType()
        {
            Name = "PostToDelete";
            Field<NonNullGraphType<IntGraphType>>("Id");
        }
    }
}
