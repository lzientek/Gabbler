using System.ComponentModel.DataAnnotations;

namespace Gabbler.gApi.Models.Users
{
    public class UserInscriptionModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100,ErrorMessage = "Mail must have maximum 100 chars.")]
        public string Mail { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Pseudo must have maximum 100 chars.")]
        public string Pseudo { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "First name must have maximum 100 chars.")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Last name must have maximum 100 chars.")]
        public string LastName { get; set; }

    }
}
