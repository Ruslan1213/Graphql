﻿using GraphQL.Types;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Models.GraphQlModels.PostModels;

namespace GraphQlLibary.Web.Models.GraphQlModels.CommentModel
{
    public class CommentType : ObjectGraphType<Comment>
    {
        public CommentType()
        {
            Name = "comment";
            Field(d => d.CommentatorName, nullable: true).Description("The name of the comment author.");
            Field(x => x.Id);
            Field(x => x.ParentCommentId);
            Field(x => x.Text);
            Field(x => x.PostId);
            Field<PostType>().Name("post").Resolve(x => x.Source.Post);
        }
    }
}
