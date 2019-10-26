using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ApiAsignarCatadorController : ApiController
    {
        private Repositorio repositorio;

        public ApiAsignarCatadorController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        // GET: api/ApiAsignarCatador
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiAsignarCatador/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiAsignarCatador
        public bool registrarCatacion(string codCatacion, string codPanel, string codCatador, string codCafe, int cantidad)
        {
            return this.repositorio.registrarCatacion( codCatacion, codPanel,codCatador,codCafe,cantidad);
        }

        // PUT: api/ApiAsignarCatador/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiAsignarCatador/5
        public void Delete(int id)
        {
        }
    }
}
