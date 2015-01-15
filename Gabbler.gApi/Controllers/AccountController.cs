using System;
using System.Web.Http;
using Gabbler.Api.Models.Users;
using Gabbler.Core;
using WebMatrix.WebData;

namespace Gabbler.Api.Controllers
{
    public class AccountController : ApiController
    {
        private DbEntities db = new DbEntities();

        [HttpPost]
        [Route("Connection")]
        [AllowAnonymous]
        public IHttpActionResult Connection([FromBody] UserConnectionModel user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (WebSecurity.Login(user.Mail, user.Password))
            {
                return Ok();
            }
            return BadRequest("Check your credentials.");
        }


        [HttpPost]
        [Route("Inscription")]
        [AllowAnonymous]
        public IHttpActionResult Inscription([FromBody] UserInscriptionModel user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usr = user.ToUser();

            try
            {
                db.Users.Add(usr);

                var result = WebSecurity.CreateAccount(user.Mail, user.Password);
                return Ok();
            }
            catch (Exception)
            {
                db.Users.Remove(usr);
                return BadRequest("DB error");
            }
            finally
            {
                db.SaveChanges();
            }

        }

    }
}
