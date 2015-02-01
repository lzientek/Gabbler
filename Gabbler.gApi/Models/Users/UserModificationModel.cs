using System.ComponentModel.DataAnnotations;

namespace Gabbler.gApi.Models.Users
{
    public class UserModificationModel
    {
        [EmailAddress]
        [StringLength(100,ErrorMessage = "Mail must have maximum 100 chars.")]
        public string Mail { get; set; }
        
        [StringLength(100, ErrorMessage = "First name must have maximum 100 chars.")]
        public string FirstName { get; set; }
        
        [StringLength(100, ErrorMessage = "Last name must have maximum 100 chars.")]
        public string LastName { get; set; }

    }
}
