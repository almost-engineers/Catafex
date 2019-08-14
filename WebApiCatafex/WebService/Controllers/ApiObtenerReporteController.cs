using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ApiObtenerReporteController : ApiController
    {
        // GET: api/ApiObtenerReporte
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiObtenerReporte/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiObtenerReporte
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiObtenerReporte/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiObtenerReporte/5
        public void Delete(int id)
        {
        }
    }
}
