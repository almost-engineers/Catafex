using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ApiGestionarEventoController : ApiController
    {
        // GET: api/ApiGestionarEvento
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiGestionarEvento/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiGestionarEvento
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiGestionarEvento/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiGestionarEvento/5
        public void Delete(int id)
        {
        }
    }
}
