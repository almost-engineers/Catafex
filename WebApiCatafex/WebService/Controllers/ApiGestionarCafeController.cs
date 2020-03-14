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
        readonly private IRepositorio repositorio;
        public ApiGestionarCafeController()
        {
            this.repositorio = FabricaRepositorio.CrearRepositorio();
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
            return this.convertirCAFE(repositorio.ConsultarCafes());
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
           
            IList<CAFE> cafesDB = repositorio.CconsultarCafes(codigo);
            IList<Cafe> cafes = new List<Cafe>();
            foreach (CAFE cafe in cafesDB)
            {
                cafes.Add(new Cafe(cafe.CODCAFE, cafe.PROCEDENCIA, cafe.ORIGEN, cafe.NOMBRE, (int)cafe.PUNTOTUESTE, (int)cafe.GRADOMOLIENDA, cafe.TIPOCAFE));

            }
            return cafes;
        }

        /// <summary>
        /// Este metodo se encarga de transformar el tipo de CAFE en Cafe de este modo, el 
        /// servicio puede devolver una lista de datos que puedan ser interpretados 
        /// posteriormente, por la parte que solicita el servicio REST
        /// </summary>
        /// <param name="cafesDB">Lista de cafes de tipo CAFE(base de datos)</param>
        /// <returns>una lista de cafes de tipo Cafe (Models)</returns>
        protected internal IList<Cafe> convertirCAFE(IList<CAFE> cafesDB)
        {
            IList<Cafe> cafes = new List<Cafe>();
            foreach (CAFE cafe in cafesDB)
            {
                cafes.Add(new Cafe(cafe.CODCAFE, cafe.PROCEDENCIA, cafe.ORIGEN, cafe.NOMBRE, (int)cafe.PUNTOTUESTE, (int)cafe.GRADOMOLIENDA, cafe.TIPOCAFE));

            }
            return cafes;
        }

        /*
         * POST: api/ApiGestionarCafe/ingresarCafe
         * Este metodo recibe como parametro los atributos propios de un cafe, asi que llama al metodo "insertarCafe" 
         * del repositorio, para registrar este nuevo cafe en la base de datos
         */
        [HttpPost]
        public bool ingresarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste)
        {
            return repositorio.InsertarCafe(nombre,tipoCafe,origen,codEvento,procedencia,gradoMolienda, puntoTueste);
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
            return repositorio.ActualizarCafe(codCafe,nombre,tipoCafe, origen, procedencia, gradoMolienda, puntoTueste);
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
            return repositorio.EliminarCafe(codCafe);
        }
    }
}
