using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Gabbler.Core;
using Gabbler.gApi.Helpers;
using Gabbler.gApi.Helpers.Auth;
using Gabbler.gApi.Helpers.ModelExtensions;
using Gabbler.gApi.Models;
using Gabbler.gApi.Models.Gabs;

namespace Gabbler.gApi.Controllers
{
    public class GabController : ApiController
    {
        private DbEntities db = new DbEntities();
        private AuthRepository rp = new AuthRepository();

        private const int NbOfGabsPerPage = 2;

        /// <summary>
        /// get a gab by id
        /// </summary>
        /// <param name="id">gab id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Gabs/{id}")]
        [ResponseType(typeof(GabDetailModel))]
        public async Task<IHttpActionResult> GetGabById([FromUri] int id)
        {
            var gab = await db.Gabs.FindAsync(id);
            if (gab == null)
            {
                return NotFound();
            }
            return Ok(gab.ToGabDetailModel());
        }

        /// <summary>
        /// get gabs from a user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Users/{userId}/Gabs")]
        [ResponseType(typeof(GabsList))]
        public async Task<IHttpActionResult> GetUsersGabs([FromUri] int userId)
        {
            return await GetUsersGabs(userId, 0);
        }

        /// <summary>
        /// Get gabs from a user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="startNumber">start gab number</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Users/{userId}/Gabs/{startNumber}")]
        [ResponseType(typeof(GabsList))]
        public async Task<IHttpActionResult> GetUsersGabs([FromUri] int userId, [FromUri] int startNumber)
        {
            var user = await db.Users.FindAsync(userId);
            return Ok(user.Gabs.OrderByDescending(g => g.CreationDate)
                .ToList().ToGabsList(startNumber, NbOfGabsPerPage));
        }

        /// <summary>
        /// Recupere les derniers gabs a partir d'une certaine plage 
        /// </summary>
        /// <param name="startNumber">gab de debut</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Gabs/Start/{startNumber}")]
        [Route("Gabs")]
        [ResponseType(typeof(GabsList))]
        public IHttpActionResult GetGabs([FromUri] int startNumber = 0)
        {
            return Ok(db.Gabs.OrderByDescending(g => g.CreationDate)
                .ToList().ToGabsList(
                startNumber, NbOfGabsPerPage));
        }

        /// <summary>
        /// Add a new gab
        /// </summary>
        /// <param name="gab">gab infos</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Gabs")]
        [Authorize]
        [ResponseType(typeof(GabDetailModel))]
        public async Task<IHttpActionResult> PostGab([FromBody] GabPostModel gab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var g = gab.ToGab();
            try
            {
                g.User = await User.GetActualUser(rp, db);
                db.Gabs.Add(g);
                await db.SaveChangesAsync();
                return Created(string.Format("/Gabs/{0}", g.Id), g.ToGabDetailModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// modify a gab
        /// </summary>
        /// <param name="id">gab id</param>
        /// <param name="gab">gab new values</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Gabs/{id}")]
        [Authorize]
        [ResponseType(typeof(GabDetailModel))]
        public async Task<IHttpActionResult> PutGab([FromUri] int id, [FromBody] GabPostModel gab)
        {
            //check values
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var g = await db.Gabs.FindAsync(id);
            if (g == null) { return NotFound(); }
            if (g.User.Pseudo != User.Identity.Name) { return Unauthorized(); }

            try
            {
                g.Message = gab.Content;
                g.ModificationDate = DateTime.Now;
                await db.SaveChangesAsync();
                return Ok(g.ToGabDetailModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// modify a gab
        /// </summary>
        /// <param name="id">gab id</param>
        /// <param name="gab">gab new values</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Gabs/{id}")]
        [Authorize]
        [ResponseType(typeof(GabDetailModel))]
        public async Task<IHttpActionResult> RemoveGab([FromUri] int id)
        {
            //check values
            var g = await db.Gabs.FindAsync(id);
            if (g == null) { return NotFound(); }
            if (g.User.Pseudo != User.Identity.Name) { return Unauthorized(); }

            try
            {
                db.Gabs.Remove(g);
                await db.SaveChangesAsync();
                return Ok(new MsgModel("Removed."));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Gabs/TimeLine")]
        [Authorize]
        [ResponseType(typeof(GabsList))]
        public async Task<IHttpActionResult> GetTimeLineGabs()
        {
            return await GetTimeLineGabs(0);
        }

        [HttpGet]
        [Route("Gabs/TimeLine/{startGab}")]
        [Authorize]
        [ResponseType(typeof(GabsList))]
        public async Task<IHttpActionResult> GetTimeLineGabs([FromUri] int startGab)
        {
            try
            {
                var usr = await User.GetActualUser(rp, db);

                var follows = usr.Follows.Select(f => f.Id_User).ToList();
                follows.Add(usr.Id);//on s'ajoute soit meme
                var gabs = db.Gabs.Where(g => follows.Contains(g.Id_Author));
                return Ok(gabs.OrderByDescending(g => g.CreationDate).ToList().ToGabsList(startGab, NbOfGabsPerPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
