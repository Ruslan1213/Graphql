using GraphQL.Types;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;

namespace GraphQlLibary.Web.Models.GraphQlModels
{
    public class PhotoType : ObjectGraphType<Photo>
    {
        public PhotoType()
        {
            Field(x => x.Id);
            Field(x => x.CloudStoreId);
            Field(x => x.PostId);
            Field<PostType, Post>().Name("post").Resolve(x => x.Source.Post);
        }
    }
}