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
        public bool registrarCatacion(string codCatacion, string codPanel, string codCatador, string codCafe, int cantidad)
        {
            return this.repositorio.registrarCatacion( codCatacion, codPanel,codCatador,codCafe,cantidad);
        }

       
    }
}
