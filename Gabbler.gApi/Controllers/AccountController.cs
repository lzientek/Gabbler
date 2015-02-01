using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
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
        private const string FileSaveLocation = "~/UserFiles";

        [HttpGet]
        [Route("Account/Me")]
        [Authorize]
        [ResponseType(typeof(UserDetailModel))]
        public async Task<IHttpActionResult> ActualUser()
        {
            var usr = await User.GetActualUser(rp, db);

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

        /// <summary>
        /// Post the background image
        /// Post file name Background
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Account/Me/Background")]
        [Authorize]
        public async Task<IHttpActionResult> PostBackgroundImage()
        {
            var usr = await User.GetActualUser(rp, db);
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    var newFile = UploadFile(httpRequest, "Background", usr.Id, ".jpg", ".png");
                    if (newFile != string.Empty)
                    {
                        usr.BackgroundImagePath = newFile;
                        usr.ModificationDate = DateTime.Now;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return new StatusCodeResult(HttpStatusCode.Created, this);
        }

        /// <summary>
        /// Post the PostProfile Image
        /// Post file name ProfileImg
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Account/Me/ProfileImage")]
        [Authorize]
        public async Task<IHttpActionResult> PostProfileImage()
        {
            var usr = await User.GetActualUser(rp, db);
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    var newFile = UploadFile(httpRequest, "ProfileImg", usr.Id, ".jpg", ".png");
                    if (!string.IsNullOrWhiteSpace(newFile))
                    {
                        usr.ModificationDate = DateTime.Now;
                        usr.ProfilePhotoPath = newFile;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return new StatusCodeResult(HttpStatusCode.Created, this);
        }


        [HttpPut]
        [Route("Account/Me")]
        [Authorize]
        [ResponseType(typeof(UserDetailModel))]
        public async Task<IHttpActionResult> PutActualUser([FromBody] UserModificationModel user)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var usr = await User.GetActualUser(rp, db);
            var identity = await User.Identity.GetIdentityUser(rp);
            try
            {
                if (!string.IsNullOrWhiteSpace(user.Mail))
                {
                    var result = await rp.ChangeMail(identity.Id, user.Mail);
                    var errors = GetErrorResult(result);
                    if (errors != null) { return errors; }
                    usr.Mail = user.Mail;
                }
                if (!string.IsNullOrWhiteSpace(user.FirstName))
                {
                    usr.FirstName = user.FirstName;
                }
                if (!string.IsNullOrWhiteSpace(user.LastName))
                {
                    usr.LastName = user.LastName;
                }
                usr.ModificationDate = DateTime.Now;
                usr.BackgroundImagePath = "lucas";
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(usr.ToUserDetailModel());
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


        /// <summary>
        /// Upload d'image
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileName"></param>
        /// <param name="id"></param>
        /// <param name="validExtension"></param>
        /// <returns></returns>
        private string UploadFile(HttpRequest request, string fileName, int id, params string[] validExtension)
        {
            var postedFile = request.Files[fileName];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string extension = Path.GetExtension(postedFile.FileName);
                if (validExtension.Length != 0 && !validExtension.Contains(extension))
                {
                    throw new BadImageFormatException(string.Format("Extension du fichier non valide. ({0})", string.Join(", ", validExtension)), fileName);
                }
                string path = string.Format("{0}/{1}{2}{3}", HttpContext.Current.Server.MapPath(FileSaveLocation), fileName, id, extension);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                postedFile.SaveAs(path);
                return string.Format("{0}/{1}{2}{3}", "/UserFiles", fileName, id, extension); ;
            }
            return string.Empty;
        }

    }
}
