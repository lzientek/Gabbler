using System.Collections.Generic;

namespace Gabbler.gApi.Models.Comments
{
    public class CommentList
    {
        public int TotalComments { get; set; }
        public int NbOfShownComments { get; set; }
        public int StartComment { get; set; }
        public List<CommentModel>  Comments { get; set; }
    }
}