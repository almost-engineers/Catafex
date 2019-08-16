using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ApiRegistrarCatadorController : ApiController
    {
        // GET: api/ApiRegistrarCatador
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiRegistrarCatador/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiRegistrarCatador
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiRegistrarCatador/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiRegistrarCatador/5
        public void Delete(int id)
        {
        }
    }
}
