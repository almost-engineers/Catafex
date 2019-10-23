using Persistencia;
using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [HttpGet]
        public IEnumerable<Catacion> consultarCatacion(string codCatador)
        {
            return convertirCATACION(repositorio.consultarCatacionesAsignadas(codCatador));
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
        [Route("api/ApiRegistrarCata/")]
        public HttpResponseMessage registrarCata(Cata cata)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                repositorio.registrarCata(cata.codCata, cata.rancidez, cata.dulce,
                  cata.acidez, cata.cuerpo, cata.aroma, cata.amargo, cata.impresionGlobal,
                  cata.fragancia, cata.saborResidual, cata.observaciones);
                return response;
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


    }
}
