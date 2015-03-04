using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Gabbler.Core;
using Gabbler.gApi.Helpers;
using Gabbler.gApi.Helpers.Auth;
using Gabbler.gApi.Helpers.ModelExtensions;
using Gabbler.gApi.Models.Comments;

namespace Gabbler.gApi.Controllers
{
    public class CommentController : ApiController
    {
        public const int MaxCommentFirst = 2;
        public const int MaxComment = 10;


        private DbEntities db = new DbEntities();
        private AuthRepository rp = new AuthRepository();

        [HttpGet]
        [Route("Gabs/{id}/Comments")]
        [ResponseType(typeof (CommentList))]
        public async Task<IHttpActionResult> CommentsFromGab([FromUri] int id)
        {
            var gab = await db.Gabs.FindAsync(id);
            if (gab == null) { return NotFound(); }
            return Ok(gab.Comments.OrderByDescending(c => c.Id).ToList().ToCommentsList(0, MaxCommentFirst));
        }

        [HttpGet]
        [Route("Gabs/{id}/Comments/{startComment}")]
        [ResponseType(typeof(CommentList))]
        public async Task<IHttpActionResult> CommentsFromGab([FromUri] int id,[FromUri] int startComment)
        {
            var gab = await db.Gabs.FindAsync(id);
            if (gab == null) { return NotFound(); }
            return Ok(gab.Comments.OrderByDescending(c=>c.Id).ToList().ToCommentsList(startComment, MaxComment));
        }

        [HttpPost]
        [Route("Gabs/{id}/Comments")]
        [ResponseType(typeof(CommentModel))]
        [Authorize]
        public async Task<IHttpActionResult> CommentAGab([FromUri] int id, [FromBody] CommentPostModel comment)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var gab = await db.Gabs.FindAsync(id);
            if (gab == null) { return NotFound(); }
            try
            {
                var usr = await User.GetActualUser(rp, db);
                var com = comment.ToComment();
                com.User = usr;
                com.Gab = gab;
                gab.Comments.Add(com);
                await db.SaveChangesAsync();
                return Ok(com.ToCommentModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Comments/{id}")]
        [Authorize]
        public async Task<IHttpActionResult> EditComment([FromUri] int id, [FromBody] CommentPostModel comment)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var com = await db.Comments.FindAsync(id);
            if (com == null) { return NotFound(); }
            if (com.User.Pseudo != User.Identity.Name) { return Unauthorized(); }
            try
            {
                com.Message = comment.Message;
                com.ModificationDate = DateTime.Now;
                await db.SaveChangesAsync();
                return Ok(com.ToCommentModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Comments/{id}")]
        [Authorize]
        public async Task<IHttpActionResult> DeleteComment([FromUri] int id)
        {
            var com = await db.Comments.FindAsync(id);
            if (com == null) { return NotFound(); }
            if (com.User.Pseudo != User.Identity.Name) { return Unauthorized(); }
            try
            {
                db.Comments.Remove(com);
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
