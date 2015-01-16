using Microsoft.AspNet.Identity.EntityFramework;

namespace Gabbler.gApi.Helpers
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("WebSecConnection")
        {

        }
    }
}