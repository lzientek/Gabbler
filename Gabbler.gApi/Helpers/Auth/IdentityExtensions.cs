using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Gabbler.Core;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gabbler.gApi.Helpers.Auth
{
    public static class IdentityExtensions
    {
        
        public static async Task<User> GetActualUser(this IPrincipal uPrincipal,AuthRepository rp,DbEntities db)
        {
            var result = await uPrincipal.Identity.GetIdentityUser(rp);
            var usr = await db.Users.SqlQuery("SELECT * FROM dbo.Users WHERE dbo.Users.ConnectionId = @id", new SqlParameter("id", result.Id)).FirstOrDefaultAsync();
            return usr;
        }

        public static async Task<IdentityUser> GetIdentityUser(this IIdentity identity ,AuthRepository rp)
        {
            var result = await rp.FindUser(identity.Name);
            return result;
        }
    }
}