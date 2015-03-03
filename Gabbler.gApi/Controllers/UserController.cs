using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Gabbler.Core;
using Gabbler.gApi.Helpers;
using Gabbler.gApi.Helpers.Auth;
using Gabbler.gApi.Helpers.ModelExtensions;
using Gabbler.gApi.Models;
using Gabbler.gApi.Models.Users;

namespace Gabbler.gApi.Controllers
{
    public class UserController : ApiController
    {
        private DbEntities db = new DbEntities();
        private AuthRepository rp = new AuthRepository();

        [HttpGet]
        [Route("Users/{id}")]
        [ResponseType(typeof(UserDetailModel))]
        public async Task<IHttpActionResult> GetUser([FromUri]int id)
        {
            var usr = await db.Users.FindAsync(id);
            return Ok(usr.ToUserDetailModel());
        }

        [HttpGet]
        [Route("Users/{id}/Follows")]
        [ResponseType(typeof(IEnumerable<UserBasicModel>))]
        public async Task<IHttpActionResult> GetFollows([FromUri]int id)
        {
            var usr = await db.Users.FindAsync(id);
            return Ok(usr.Follows.FollowsToUserBasicModel());
        }

        [HttpGet]
        [Route("Users/{id}/Followers")]
        [ResponseType(typeof(IEnumerable<UserBasicModel>))]
        public async Task<IHttpActionResult> GetFollowers([FromUri]int id)
        {
            var usr = await db.Users.FindAsync(id);
            if (usr == null) { return NotFound(); }

            return Ok(usr.Followers.FollowersToUserBasicModel());
        }

        /// <summary>
        /// Follow some one
        /// </summary>
        /// <param name="followId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Follow/{followId}")]
        [Authorize]
        [ResponseType(typeof(IEnumerable<UserBasicModel>))]
        public async Task<IHttpActionResult> Follow([FromUri]int followId)
        {
            var usr = await User.GetActualUser(rp, db);
            var follow = await db.Users.FindAsync(followId);
            if (usr == null || follow == null) { return NotFound(); }

            try
            {
                //verify si on follow pas deja
                var check = usr.Follows.FirstOrDefault(f => f.Id_User == followId);
                if (check != null) { return Content(HttpStatusCode.Conflict, new MsgModel("Already followed.")); }

                usr.Follows.Add(new Follow() { Follower = usr, User = follow });
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(usr.Follows.FollowsToUserBasicModel());
        }

        /// <summary>
        /// unfollow some one
        /// </summary>
        /// <param name="followId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("UnFollow/{followId}")]
        [Authorize]
        [ResponseType(typeof(IEnumerable<UserBasicModel>))]
        public async Task<IHttpActionResult> UnFollow([FromUri]int followId)
        {
            var usr = await User.GetActualUser(rp, db);
            if (usr == null) { return NotFound(); }

            try
            {
                var follow = usr.Follows.FirstOrDefault(f => f.Id_User == followId);
                if (follow == null) { return NotFound(); }

                usr.Follows.Remove(follow);
                db.Follows.Remove(follow);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(usr.Follows.FollowsToUserBasicModel());
        }
    }
}
