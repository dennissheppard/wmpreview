using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WMPReview.DAL;

namespace WmpReview.Api.Controllers
{
    public class ThrowAwayControllerController : ApiController
    {
        // GET: api/ThrowAwayController
        public IEnumerable<string> Get()
        {
            var awesomedb = new WMPFoodAppEntities();
            awesomedb.SearchForBusinessInRadius(0.732877678433442, -1.50913507728434, 100000, -10000, -10000, 10000, 10000);

            return new List<string>();
        }

        // GET: api/ThrowAwayController/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ThrowAwayController
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ThrowAwayController/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ThrowAwayController/5
        public void Delete(int id)
        {
        }
    }
}
