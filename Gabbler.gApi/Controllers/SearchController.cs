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
        [Route("BasicSearch/{text}")]
        [ResponseType(typeof(SearchListModel))]
        public async Task<IHttpActionResult> GetSearch([FromUri] string text)
        {
            return await GetSearch(text, 3, 3);
        }

        [HttpGet]
        [Route("Search/{text}/{numberUser}/{numberGab}")]
        [ResponseType(typeof(SearchListModelExtend))]
        public async Task<IHttpActionResult> GetSearch([FromUri] string text, [FromUri] int numberUser, [FromUri] int numberGab)
        {
            try
            {
                var userSearchs = db.Users.Where(x => x.LastName.Contains(text) || x.FirstName.Contains(text) || x.Pseudo.Contains(text)).ToList();
                var gabSearchs = db.Gabs.Where(x => x.Message.Contains(text)).ToList();

                SearchListModelExtend searchResult = new SearchListModelExtend
                {
                    NbMaxUser = numberUser,
                    NbMaxGab = numberGab,
                    NbResultUser = userSearchs.Count,
                    NbResultGabs = gabSearchs.Count,
                    ListOfUser = userSearchs.Take(numberUser).ToUserBasicModel(),
                    ListOfGab = gabSearchs.Take(numberGab).ToGabBasicModel(),
                };
                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}