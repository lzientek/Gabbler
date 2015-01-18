using System.Threading.Tasks;
using System.Web.Http;
using Gabbler.Core;
using Gabbler.gApi.Helpers;
using Gabbler.gApi.Helpers.ModelExtensions;
using Gabbler.gApi.Models.Users;

namespace Gabbler.gApi.Controllers
{
    public class UserController : ApiController
    {
        private DbEntities db = new DbEntities();
        private AuthRepository rp = new AuthRepository();

        [HttpGet]
        [Route("Users/{id}")]
        [Authorize]
        public async Task<IHttpActionResult> GetUser([FromUri]int id)
        {
            var usr = await db.Users.FindAsync(id);
            return Ok(usr.ToUserDetailModel());
        }

        [HttpGet]
        [Route("Users/{id}/Follows")]
        [Authorize]
        public async Task<IHttpActionResult> GetFollows([FromUri]int id)
        {
            var usr = await db.Users.FindAsync(id);
            return Ok(usr.Follows.FollowsToUserBasicModel());
        }

        [HttpGet]
        [Route("Users/{id}/Followers")]
        [Authorize]
        public async Task<IHttpActionResult> GetFollowers([FromUri]int id)
        {
            var usr = await db.Users.FindAsync(id);
            return Ok(usr.Followers.FollowersToUserBasicModel());
        }
    }
}
