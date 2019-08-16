using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia;
using Persistencia.Entity;
using WebService.Models;

namespace WebService.Controllers
{
    public class ApiGestionarCafeController : ApiController
    {
        private Repositorio repositorio;
        public ApiGestionarCafeController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        /*
         *  GET: api/ApiGestionarCafe/consultarCafes
         *  Este metodo hace uso del metodo convertirCAFE para trasformar un dato de tipo CAFE proveniente de Entity a un
         *  tipo Cafe de la capa de Dominio.Envia como parametro para convertir el resultado de llamar a consultar paneles
         *  implementado en alguno de los dos repositorios
         */

        [HttpGet]
        public IEnumerable<Cafe> consultarCafes()
        {
            return this.convertirCAFE(repositorio.consultarCafes());
        }

        /*
         * GET: api/ApiGestionarCafe/consultarCafes
         *  Este metodo recibe como parametro un codigo el cual hace referencia al codigo de  un cafe, asi que se llama al metodo del 
         *  repositorio para luego  añadirse a la la lista de cafes(capa Dominio), utilizando el metodo convertirCAFE 
         *  el cual transforma la lista de tipo CAFE(entity) al Cafe de capa de dominio  
         */
        [HttpGet]
        public IEnumerable<Cafe> consultarCafes(string codigo)
        {
            return this.convertirCAFE(repositorio.consultarCafes(codigo));
        }

        /*
         * POST: api/ApiGestionarCafe/ingresarCafe
         * Este metodo recibe como parametro los atributos propios de un cafe, asi que llama al metodo "insertarCafe" 
         * del repositorio, para registrar este nuevo cafe en la base de datos
         */
        [HttpPost]
        public bool ingresarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste)
        {
            return repositorio.insertarCafe( nombre,tipoCafe,origen,codEvento,procedencia, (int) gradoMolienda, (int) puntoTueste);
        }

        /*
         * PUT: api/ApiGestionarCafe/actualizarCafe
         * Este metodo se encarga de actualizar un cafe registrado con anterioridad, para esto recibe en los parametros
         * del cafe que deseamos actualizar, para esto buscarermos el cafe por su codigo , junto al codigo estaran
         * valores nuevos que tendra este cafe, despues se llamara al metodo del repositorio encargado de actualizar este cafe  
         */ 
        [HttpPut]
        public bool actualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste)
        {
            return repositorio.actualizarCafe(codCafe,nombre,tipoCafe, origen, procedencia, (int)gradoMolienda,(int) puntoTueste);
        }

        /*
         * DELETE: api/ApiGestionarCafe/eliminarCafe
         * Este metodo se encarga de eliminar un cafe de la base de datos, siempre y cuando exista,para esto 
         * recibe como parametro el codigo del  cafe a eliminar, luego se llamara al metodo encargado de eliminar 
         * el cafe el cual se encuentra en el repositorio 
         */
        [HttpDelete]
        public bool eliminarCafe(string codCafe)
        {
            return repositorio.eliminarCafe(codCafe);
        }

        private IList<Cafe> convertirCAFE(IList<CAFE> cafesDB)
        {
            IList<Cafe> cafes = new List<Cafe>();
            foreach (CAFE cafe in cafesDB)
            {
                cafes.Add(new Cafe(cafe.CODCAFE, cafe.PROCEDENCIA, cafe.ORIGEN, cafe.NOMBRE, (int)cafe.PUNTOTUESTE, (int)cafe.GRADOMOLIENDA));
                
            }
            return cafes;
        }

        public string generarCodigo()
        {
            string codigo="";

            return codigo;
        }
    }
}
