using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia;

namespace WebService.Controllers
{
    public class ApiGestionarCafeController : ApiController
    {
        private Repositorio repositorio;

         public ApiGestionarCafeController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        public IEnumerable<Cafe> consultarCafes(){
         }
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

       private IList<Cafe> convertirCAFE(IList<CAFE> cafesDB)
        {
            IList<Cafe> cafes = new List<Cafe>();
            foreach (CAFE cafe in cafesDB)
            {
                cafes.Add(new Cafe(cafe.CODCAFE, cafe.PROCEDENCIA, cafe.ORIGEN, cafe.NOMBRE, (int)cafe.PUNTOTUESTE, (int)cafe.GRADOMOLIENDA));
                
            }
         return cafes;
        }
    }
}
