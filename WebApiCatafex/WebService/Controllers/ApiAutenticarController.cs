using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ApiAutenticarController : ApiController
    {
        // GET: api/ApiAutenticar
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiAutenticar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiAutenticar
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiAutenticar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiAutenticar/5
        public void Delete(int id)
        {
        }
    }
}
