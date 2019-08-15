using Persistencia;
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
        


        public ApiRegistrarCataController() {
            repositorio = FabricaRepositorio.crearRepositorio();
        }
        private IList<Cata> consultarCatacion() {
       
            return null;
        }
        private Cata obtenerCata() {
          

            return null;
        }
        private bool registrarCata(int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, 
            int impresionGlobal, int fragancia, int saborResidual, string observaciones) {

            return repositorio.registrarCata( rancidez,  dulce,  acidez,  cuerpo,  aroma,  amargo,
             impresionGlobal,  fragancia,  saborResidual,  observaciones);
        }

        public bool validarDatos() {
            return true;
        }
 
       
    }
}
