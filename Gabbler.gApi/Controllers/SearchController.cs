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
using Gabbler.gApi.Models.Search ;

namespace Gabbler.gApi.Controllers
{
    public class SearchController : ApiController
    {
        private DbEntities db = new DbEntities();

        [HttpGet]
        [Route("Search/{text}")]
        [ResponseType(typeof(SearchListModel))]
        public async Task<IHttpActionResult> GetSearch([FromUri] string text)
        {
            try
            {
                var userSearchs = db.Users.Where(x => x.LastName.Contains(text)).ToList();
                var gabSearchs = db.Gabs.Where(x => x.Message.Contains(text)).ToList();

                SearchListModel searchResult = new SearchListModel();
                searchResult.ListOfUser = userSearchs.ToUserBasicModel();
                searchResult.ListOfGab = gabSearchs.ToGabsList(0, 3);

                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}