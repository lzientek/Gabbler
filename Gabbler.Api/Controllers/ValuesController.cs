using System.Collections.Generic;
using System.Web.Http;
using Gabbler.Core;

namespace Gabbler.Api.Controllers
{
    public class ValuesController : ApiController
    {
        DbEntities db = new DbEntities(); 
        [Route("values")]
        public IEnumerable<string> Get()
        {
            var gabs = db.Gabs;
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
