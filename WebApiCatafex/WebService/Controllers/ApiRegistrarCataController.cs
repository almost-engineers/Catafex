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
        /// <summary>
        /// Este metodo se encarga de obtener la informacion correspondiente a una catacion a partir de
        /// su codigo. Esta informacion es convertida en formato json y es devuelta junto con una respuesta
        /// de validacion, esta ultima corresponde a un HttpResponseMessage
        /// </summary>
        /// <param name="codCatacion">Codigo de la catacion</param>
        /// <returns>Un HttpResponseMessage con la informacion de la cacatacion, y un OK si el codigo enviado
        /// fue correcto de lo contrario la respuesta contendra un BadGateway y un mensaje de null</returns>
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
        /// <summary>
        /// Este metodo permite construtir una cata a partir de un diccionario de datos en el cual
        /// se encuentra la informacion de una cata
        /// </summary>
        /// <param name="catas">Diccionario de datos en el que la llave es un identificador perteneciente
        /// a un dato de la cata y su llave el valor correspondiente</param>
        /// <returns>Retorna una cata con toda su informacion</returns>
        private Catas convertirCata(Dictionary<string, string> catas) {

            Catas c_catas = new Catas(catas["CodCafe"], int.Parse(catas["cantVez"]), catas["hora"], catas["fecha"], catas["tipoCafe"], catas["atributos"]);
            return c_catas;
        }
        /// <summary>
        /// Este metodo se encarga de listar todas las cataciones que un catador tiene asignadas, puede tener
        /// dos respuestas, OK si todo salio bien, BadGateway si el catador no tiene catas asignadas, o el codigo 
        /// de este es incorrecto 
        /// </summary>
        /// <param name="codCatador">Codigo del catador</param>
        /// <returns>Retorna un HttpResponseMessage con un status code con valor OK con la informacion si el 
        /// codigo del catador corresponde a un codigo correcto y si cuenta con cataciones asignadas</returns>
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
        /// <summary>
        /// Este metodo permite trasformar la informacion proveniente de la base de datos con un
        /// formato que no puede ser interpretado,en un objeto Catacion que si se puede interpretar por 
        /// el cliente que realiza la solicitud REST
        /// </summary>
        /// <param name="catacionesDB">Lista de cataciones con informacion de tipo de base de datos</param>
        /// <returns>Restrona una lista con las cataciones convertidas en un formato que pude ser
        /// interpretado, es decir, en un objeto Catacion (Models)</returns>
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
        /// <summary>
        /// Este metodo permite convertir una cata de la base de datos en un objeto de tipo Cata, 
        /// con el fin de que pueda ser interpretada, dicha cata es consultada en el repositorio
        /// a partir de su codigo
        /// </summary>
        /// <param name="codigo">Codigo de la cata</param>
        /// <returns>Retorna una cata con la informacion del repositorio</returns>
        protected internal Cata convertirCATA(string codigo)
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

        // POST: api/ApiRegistrarCata
        /// <summary>
        /// Este metodo se encarga de registrar la informacion proveniente de una cata en el respositorio,
        /// esta operacion puede obtener tres posibles resultados, Ok si la operacion de registro fue 
        /// exitosa, BadRequest si algun dato es incorrecto y finalmente BadGateway si la operacion no pudo
        /// ser realizada de manera exitosa
        /// </summary>
        /// <param name="cata">Cata con todos los datos enviados por el catador</param>
        /// <returns>Retorna HttpResponseMessage, con la informacion de status code OK, BadRequest, BadGateway
        /// dependiendo el resultado de la operacion</returns>
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
        /// <summary>
        /// Este metodo se encarga de actualizar los datos de una catacion, con la informacion proveniente 
        /// del cliente, esta operacion puede obtener tres respuestas, OK si todo salio bien, NotFound si
        /// los datos enviados son nulos o BadGateway si sucede algun error durante la actualizacion
        /// </summary>
        /// <param name="catacion">Catacion con los datos para ser actualizados en el repositorio</param>
        /// <returns>Retorna HttpResponseMessage, con la informacion de status code OK, NotFound, BadGateway
        /// dependiendo el resultado de la operacion</returns>
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
