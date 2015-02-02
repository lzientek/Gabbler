using System;
using System.Collections.Generic;
using Gabbler.Core;
using Gabbler.gApi.Models.Comments;
using Gabbler.gApi.Models.Gabs;

namespace Gabbler.gApi.Helpers.ModelExtensions
{
    internal static class CommentsExtensions
    {
        internal static CommentList ToCommentsList(this List<Comment> comments,int startNumber, int maxQuantity)
        {
            //creation de l'objet basic
            var commentList = new CommentList()
            {
                TotalComments = comments.Count,
                StartComment = startNumber,
                Comments = new List<CommentModel>(),
            };

            //ajout du nombre de comments voulu
            int compteur = 0;
            for (int i = 0; i < maxQuantity
                && (i + startNumber) < comments.Count; i++)
            {
                compteur++;
                commentList.Comments.Add(comments[startNumber + i].ToCommentModel());
            }

            //nbde comment ajouté
            commentList.NbOfShownComments = compteur;
            return commentList;
        }

        internal static CommentModel ToCommentModel(this Comment comment)
        {
            return new CommentModel()
            {
                
                Message = comment.Message,
                Id = comment.Id,
                User = comment.User.ToUserBasicModel(),
                CreationDate = comment.CreationDate,
                ModificationDate = comment.ModificationDate
            };
        }
        
        internal static Comment ToComment(this CommentPostModel comment)
        {
            return new Comment()
            {
                CreationDate = DateTime.Now,
                Message = comment.Message,
            };
        }
    }
}