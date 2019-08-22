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

        [HttpGet]

        public string getCafe(string codigoCatacion)
        {

            return repositorio.obtenerTipoCafe(codigoCatacion);
        }

        [HttpGet]

        public string obtenerAtributosCafes(string tipoCafe)
        {
            return repositorio.obtenerAtributosCafes(tipoCafe);
        }
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

        [HttpPost]
        public bool registrarCata1(string a) {
            return repositorio.registrarCata();
        }
        
        public bool registrarCata(string codCatacion, string vezCatada, string rancidez, string dulce, string acidez,
            string cuerpo, string aroma, string amargo, string impresionGlobal, string fragancia, string saborResidual,
            string observaciones)
        {
            if (validarDatos())
            {

                return
                  repositorio.registrarCata(codCatacion, int.Parse(rancidez), int.Parse(dulce),
                  int.Parse(acidez), int.Parse(cuerpo), int.Parse(aroma), int.Parse(amargo), int.Parse(impresionGlobal),
                  int.Parse(fragancia), int.Parse(saborResidual), observaciones);
            }
            else return false;
        }

        public bool validarDatos()
        {
            return true;
        }


    }
}
