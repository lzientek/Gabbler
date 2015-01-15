using System;
using System.Threading.Tasks;
using System.Web.Http;
using Gabbler.gApi.Helpers;
using Gabbler.gApi.Models.Users;
using Gabbler.Core;
using Microsoft.AspNet.Identity;

namespace Gabbler.gApi.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        private DbEntities db = new DbEntities();
        private AuthRepository rp = new AuthRepository();

        [HttpPost]
        [Route("Connection")]
        [AllowAnonymous]
        public IHttpActionResult Connection([FromBody] UserConnectionModel user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return BadRequest("Check your credentials.");
        }


        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(UserInscriptionModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await rp.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }
            try
            {
                var usr = await rp.FindUser(userModel.Mail);
                db.Users.Add(userModel.ToUser(usr.Id));
                db.SaveChanges();

            }
            catch (Exception)
            {
                BadRequest("Db error");
            }
            return Ok();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rp.Dispose();
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

    }
}
