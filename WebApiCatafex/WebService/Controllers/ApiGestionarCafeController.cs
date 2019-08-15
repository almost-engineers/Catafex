using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ApiGestionarCafeController : ApiController
    {
        // GET: api/ApiGestionarCafe
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiGestionarCafe/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiGestionarCafe
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiGestionarCafe/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiGestionarCafe/5
        public void Delete(int id)
        {
        }

        public string generarCodigo()
        {
            string codigo="";

            return codigo;
        }
    }
}
