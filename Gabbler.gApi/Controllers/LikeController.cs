using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Gabbler.Core;
using Gabbler.gApi.Helpers;
using Gabbler.gApi.Helpers.Auth;
using Gabbler.gApi.Helpers.ModelExtensions;
using Gabbler.gApi.Models.Users;

namespace Gabbler.gApi.Controllers
{
    public class LikeController : ApiController
    {
        private DbEntities db = new DbEntities();
        private AuthRepository rp = new AuthRepository();
        [HttpGet]
        [Route("Gabs/{id}/Likes")]
        [ResponseType(typeof(IEnumerable<UserBasicModel>))]
        [Authorize]
        public async Task<IHttpActionResult> GetUsersThatLike([FromUri] int id)
        {
            var gab = await db.Gabs.FindAsync(id);
            if (gab == null) { return NotFound(); }
            var usr = await User.GetActualUser(rp, db);
            try
            {
                var users = gab.Likes.Select(l=>l.User);
                return Ok(users.ToUserBasicModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("Gabs/{id}/Like")]
        [Authorize]
        public async Task<IHttpActionResult> LikeAGab([FromUri] int id)
        {
            var gab = await db.Gabs.FindAsync(id);
            if (gab == null) { return NotFound(); }
            var usr = await User.GetActualUser(rp, db);
            try
            {
                gab.Likes.Add(new Like() { User = usr });
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Gabs/{id}/UnLike")]
        [Authorize]
        public async Task<IHttpActionResult> UnLikeAGab([FromUri] int id)
        {
            var gab = await db.Gabs.FindAsync(id);
            if (gab == null) { return NotFound(); }
            var usr = await User.GetActualUser(rp, db);
            try
            {
                var like = gab.Likes.FirstOrDefault(l => l.Id_User == usr.Id);
                if (like == null) { return BadRequest("You don't like the gab."); }

                db.Likes.Remove(like);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
