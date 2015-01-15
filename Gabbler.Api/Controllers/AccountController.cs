using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var result = WebSecurity.CreateAccount(user.Mail, user.Password);
            if ( !string.IsNullOrEmpty(result))
            {
                try
                {
                    var usr = user.ToUser();
                    db.Users.Add(usr);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    BadRequest("DB error");
                }
                return Ok();
            }
            return BadRequest("Check your credentials.");
        }

    }
}
