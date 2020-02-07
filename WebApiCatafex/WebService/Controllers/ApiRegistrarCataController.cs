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
using WebService.Models;

namespace WebService.Controllers
{
    public class ApiRegistrarCataController : ApiController
    {


        Repositorio repositorio;

        public ApiRegistrarCataController()
        {
            repositorio = FabricaRepositorio.crearRepositorio();
        }

       
        [Route("api/ApiRegistrarCata/ObtenerInformacionCatacion/{codCatacion}")]
        [HttpGet]
        public HttpResponseMessage ObtenerInformacionCatacion(string codCatacion)
        {

            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(convertirCata(repositorio.obtenerInformacionCatacion(codCatacion))));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }


        private Catas convertirCata(Dictionary<string, string> catas) {

            Catas c_catas = new Catas(catas["CodCafe"], int.Parse(catas["cantVez"]), catas["hora"], catas["fecha"], catas["tipoCafe"], catas["atributos"]);

            return c_catas;
        }
        [HttpGet]
        [Route("api/ApiRegistrarCata/{codCatador}")]
        public HttpResponseMessage consultarCatacion(string codCatador)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                IList<Catacion> catas = convertirCATACION(repositorio.consultarCatacionesAsignadas(codCatador));
                if (catas != null)
                {
                    response.Content = new StringContent(JsonConvert.SerializeObject(convertirCATACION(repositorio.consultarCatacionesAsignadas(codCatador))));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadGateway);
                }
               
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
            
        }

        //[HttpGet]

        //public Cafe getCafe(string codigoCatacion)
        //{

        //    return repositorio.obtegerTipoCafe(codigoCatacion);
        //}

        //[HttpGet]

        //public string obtenerAtributosCafes(string tipoCafe)
        //{
        //    return repositorio.obtenerAtributosCafes(tipoCafe);
        //}
        private IList<Catacion> convertirCATACION(IList<CATACION> catacionesDB)
        {

            IList<Catacion> cataciones = new List<Catacion>();

            foreach (CATACION catDB in catacionesDB)
            {
                cataciones.Add(new Catacion()
                {
                    codCatacion = catDB.CODCATACION,
                    codPanel = catDB.CODPANEL,
                    codCatador = catDB.CODCATADOR,
                    codCafe = catDB.CODCAFE,
                    cantidad = catDB.CANTIDAD.GetValueOrDefault()
                });
            }
            return cataciones;
        }
        private Cata convertirCATA(string codigo)
        {
            CATA cataDB = repositorio.consultarCata(codigo);
            Cata cata = new Cata(

                cataDB.CODCATACION,
                cataDB.VEZCATADA,
                cataDB.RANCIDEZ.GetValueOrDefault(),
                cataDB.DULCE.GetValueOrDefault(),
                cataDB.ACIDEZ.GetValueOrDefault(),
                cataDB.AROMA.GetValueOrDefault(),
                cataDB.AMARGO.GetValueOrDefault(),
                cataDB.FRAGANCIA.GetValueOrDefault(),
                cataDB.SABORESIDUAL.GetValueOrDefault(),
                cataDB.CUERPO.GetValueOrDefault(),
                cataDB.IMPRESIONGLOBAL.GetValueOrDefault(),
                cataDB.OBSERVACIONES
            );




            return cata;
        }
        private Cata obtenerCata()
        {
            return null;
        }

        // POST: api/ApiRegistrarCata
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cata"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ApiRegistrarCata/registrarCata/")]
        public HttpResponseMessage registrarCata(Cata cata)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                if (repositorio.registrarCata(cata.codCata, cata.rancidez, cata.dulce,
                  cata.acidez, cata.cuerpo, cata.aroma, cata.amargo, cata.impresionGlobal,
                  cata.fragancia, cata.saborResidual, cata.observaciones))
                {

                    return response;
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

        public bool validarDatos()
        {
            return true;
        }

        [HttpPut]
        [Route("api/ApiRegistrarCata/actualizarCatacion/")]
        public HttpResponseMessage actualizarCatacion(Catacion catacion)
        {
            if(catacion == null)
            {
                new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            try
            {
                this.repositorio.actualizarCatación(catacion.codCatacion,catacion.codCafe,catacion.codPanel,catacion.codCatador,catacion.cantidad);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
    }
}
