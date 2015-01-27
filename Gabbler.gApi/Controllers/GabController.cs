using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Gabbler.Core;
using Gabbler.gApi.Helpers.ModelExtensions;
using Gabbler.gApi.Models.Gabs;
using Gabbler.gApi.Models.Users;

namespace Gabbler.gApi.Controllers
{
    public class GabController : ApiController
    {
        private DbEntities db = new DbEntities();

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
            var gab =await db.Gabs.FindAsync(id);
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
            return await GetUsersGabs(userId,0);
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
                .ToList().ToGabsList(
                startNumber>0?startNumber-1:0, NbOfGabsPerPage));
        }

        /// <summary>
        /// Recupere les dernier gabs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Gabs")]
        [ResponseType(typeof(GabsList))]
        public IHttpActionResult GetGabs()
        {
            return GetGabs(0);
        }

        /// <summary>
        /// Recupere les derniers gabs a partir d'une certaine plage 
        /// </summary>
        /// <param name="startNumber">gab de debut</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Gabs/Start/{startNumber}")]
        [ResponseType(typeof(GabsList))]
        public IHttpActionResult GetGabs([FromUri] int startNumber)
        {
            return Ok(db.Gabs.OrderByDescending(g => g.CreationDate)
                .ToList().ToGabsList(
                startNumber > 0 ? startNumber - 1 : 0, NbOfGabsPerPage));
        }
    }
}
