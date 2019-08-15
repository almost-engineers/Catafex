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
        private IList<Cata> consultarCatacion()
        {

            return null;
        }

        private Cata convertirCATA(string codigo)
        {
            CATA cataDB = repositorio.consultarCata(codigo);
            Cata cata = new Cata(
            cataDB.CODCATACION,
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
        private bool registrarCata(string codCatacion, string vezCatada, string rancidez, string dulce, string acidez,
            string cuerpo, string aroma, string amargo, string impresionGlobal, string fragancia, string saborResidual,
            string observaciones)
        {
            if (validarDatos())
            {
                return
                  repositorio.registrarCata(codCatacion, int.Parse(vezCatada), int.Parse(rancidez), int.Parse(dulce),
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
