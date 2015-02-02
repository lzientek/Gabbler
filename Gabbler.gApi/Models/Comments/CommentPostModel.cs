using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gabbler.gApi.Models.Comments
{
    public class CommentPostModel
    {
        [Required]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Comment must have maximum 255 chars.")]
        public string Message { get; set; }

    }
}