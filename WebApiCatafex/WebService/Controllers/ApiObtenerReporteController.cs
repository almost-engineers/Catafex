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

        public ApiObtenerReporteController(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public ApiObtenerReporteController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        /// <summary>
        /// Este metodo se encarga de devolver un array de bytes, en este array se encuentra almacenada toda la informacion
        /// del grafico de estrella, en este se encuentran dos diferentes series, una serie para el patron, previamente establecido
        /// y otrra para el promedio de catas obtenido de ese panel. Esta informacion solo sera devuelta si el panel ya ha finalzado.
        /// </summary>
        /// <param name="codPanel">el codigo del panel</param>
        /// <returns>un array de bytes con la informacion del grafico</returns>
        [HttpGet]
        [Route("api/Reporte/obtenerGrafico")]
        public byte[] obtenerGrafico(string codPanel)
        {
            byte[] info = null;
            if (!codPanel.Equals("") && !this.repositorio.existePanel(codPanel)) 
            {
                if (repositorio.panelTerminado(codPanel))
                {
                    info = this.repositorio.GenerarImagen(codPanel);
                    return info;
                }
                else
                {
                    return null;
                }
            }
            return null;     
        }
        /// <summary>
        /// este metodo se encarga de obtener un array con todas las observaciones que se realizaron en el panel
        /// por parte de los catadores, se descartan aquellas observaciones que solo tienen espacio en blanco
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>un arry con todas las obervaciones del panel</returns>
        [HttpGet]
        [Route("api/Reporte/obtenerObservaciones")]
        public HttpResponseMessage obtenerObservaciones(string codPanel)
        {
            HttpResponseMessage response = null;
            if (!codPanel.Equals("") && !this.repositorio.existePanel(codPanel))
            {
                if (repositorio.panelTerminado(codPanel))
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(this.repositorio.getObservaciones(codPanel)));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("Panel no terminado");
                    return response;
                }
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
            
    }
}
