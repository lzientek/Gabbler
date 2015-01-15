using System.ComponentModel.DataAnnotations;

namespace Gabbler.Api.Models.Users
{
    public class UserConnectionModel
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
