using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Gabbler.gApi.Helpers;
using Gabbler.gApi.Helpers.Auth;
using Gabbler.gApi.Helpers.ModelExtensions;
using Gabbler.gApi.Models.Users;
using Gabbler.Core;
using Microsoft.AspNet.Identity;

namespace Gabbler.gApi.Controllers
{
 
    public class AccountController : ApiController
    {
        private DbEntities db = new DbEntities();
        private AuthRepository rp = new AuthRepository();

        [HttpGet]
        [Route("Account/Me")]
        [Authorize]
        [ResponseType(typeof(UserDetailModel))]
        public async Task<IHttpActionResult> ActualUser()
        {
            var usr =await User.GetActualUser(rp,db);

            return Ok(usr.ToUserDetailModel());
        }

        [HttpPost]
        [Route("Account/Register")]
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
                var usr = await rp.FindUser(userModel.Pseudo);
                var u = userModel.ToUser();
                u.ConnectionId = usr.Id;
                u.CreationDate = DateTime.Now;
                db.Users.Add(u);
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
