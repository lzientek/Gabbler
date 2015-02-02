using System.ComponentModel.DataAnnotations;

namespace Gabbler.gApi.Models.Gabs
{
    public class GabPostModel
    {
        [Required]
        [StringLength(255,MinimumLength =0, ErrorMessage = "Gab must have maximum 255 chars.")]
        public string Content { get; set; }

    }
}