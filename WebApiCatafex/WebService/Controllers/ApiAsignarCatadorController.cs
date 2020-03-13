using Persistencia;
using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebService.Models;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ApiAsignarCatadorController : ApiController
    {
        private Repositorio repositorio;
        private ApiNotificacionController notificacion = new ApiNotificacionController();


        public ApiAsignarCatadorController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        /// <summary>
        /// Este metodo permite asignar un catador a una cata, de este modo se indica en que panel debe catar,
        /// que muestras debe catar y cuantas veces debe realizar dicho proceso
        /// </summary>
        /// <param name="codCatacion">Codigo de identificacion de la catacion</param>
        /// <param name="codPanel">El caodigo del panel en el cual se encuentra la muestra de cafe</param>
        /// <param name="codCatador">El codigo del catador encargado</param>
        /// <param name="codCafe">El codigo de la muestra del cafe que debe ser catada</param>
        /// <param name="cantidad">La cantidad de veces que el catador debe catar una muestra</param>
        /// <returns></returns>
        // POST: api/ApiAsignarCatador
        [HttpPost]
        [Route("api/ApiAsignarCatador/asignar")]
        public HttpResponseMessage asignarCatador(List<Catacion> cataciones)
        {

            if (cataciones == null) {
                return new HttpResponseMessage(HttpStatusCode.PreconditionFailed);
            }
            string correoDestino = this.repositorio.getCorreoCatador(cataciones.First().codCatador);
            string asunto = this.repositorio.construirAsuntoCorreo(cataciones.First().codPanel);
            string mensaje = this.repositorio.construirMensajeCorreo(this.convertirCatacion(cataciones));
            try
            {
              foreach (Catacion catacion in cataciones)
                {
                    if(!this.repositorio.registrarCatacion(catacion.codPanel, catacion.codCatador, catacion.codCafe, catacion.cantidad))
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }          
                }
                this.notificacion.enviarNotificacion(correoDestino, asunto, mensaje);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        /// <summary>
        /// Metodo que se encarga de trasformar la informacion enviada por el cliente, en informacion que pueda ser
        /// leida por la base de datos
        /// </summary>
        /// <param name="cataciones">Lista de catadores asignados a las diferentes muestras de un panel</param>
        /// <returns>una lista de CATACION, elementos de la base de datos</returns>
        private List<CATACION> convertirCatacion(List<Catacion> cataciones)
        {
            List<CATACION> catacionesDb = new List<CATACION>();
            foreach (Catacion catacion in cataciones)
            {
                catacionesDb.Add(new CATACION()
                {
                    CODPANEL = catacion.codPanel,
                    CODCATADOR = catacion.codCatador,
                    CODCAFE = catacion.codCafe,
                    CANTIDAD = catacion.cantidad
                });
            }
            return catacionesDb;
        }


    }
}
