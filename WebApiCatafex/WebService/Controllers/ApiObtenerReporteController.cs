using Newtonsoft.Json;
using Persistencia;
using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebService.Models;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ApiObtenerReporteController : ApiController
    {

        private Repositorio repositorio;

        public ApiObtenerReporteController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Reporte/obtenerGrafico")]
        public byte[] obtenerGrafico(string codPanel)
        {   
            if (repositorio.panelTerminado(codPanel))
            {
                return this.repositorio.GenerarImagen(codPanel);
            }
            else
            {
                return null;
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Reporte/obtenerObservaciones")]
        public HttpResponseMessage obtenerObservaciones(string codPanel)
        {
            if (repositorio.panelTerminado(codPanel))
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(this.repositorio.getObservaciones(codPanel)));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent("Panel no terminado");
                return response;
            }
        }
            
    }
}
