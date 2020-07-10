using GraphQL.Types;
using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Models.GraphQlModels
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