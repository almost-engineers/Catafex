using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ApiRegistrarCataController : ApiController
    {
        // GET: api/ApiRegistrarCata
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiRegistrarCata/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiRegistrarCata
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiRegistrarCata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiRegistrarCata/5
        public void Delete(int id)
        {
        }
    }
}
