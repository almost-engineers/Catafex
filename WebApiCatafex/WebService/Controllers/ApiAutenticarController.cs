using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia.Entity;
using WebService.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ApiAutenticarController : ApiController
    {
        private Repositorio repositorio;
        private ApiRegistrarCatadorController controladoraRCatador;
        public ApiAutenticarController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
            controladoraRCatador = new ApiRegistrarCatadorController();
        }
        /// <summary>
        /// Este metodo permite validar los datos de un catador, de los cuales solo son requeridos 
        /// correo y contraseña para la validacion de datos de la autenticacion.
        /// </summary>
        /// <param name="catador">Recibe un catador del frontend, equivalente al catador perteneciente
        /// a la clase de la carpeta Models</param>
        /// <returns>Retorna un Mensaje de dos partes, la primer parte tiene un valor de OK si
        /// los datos de autenticacion fueron validos, y de BadGateWay en caso contrario. En la
        /// segunda parte de ser validos los datos, se retorna un Catador con todos los datos de la 
        /// base datos, en caso contrario retorna null</returns>
        // GET: api/ApiAutenticar/5
        [HttpPost]
        public HttpResponseMessage validarCamposCatador(Catador catador)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            Catador nuevoC = buscarCatador(catador.correo, catador.contrasena);
            if (nuevoC == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
            response.Content = new StringContent(JsonConvert.SerializeObject(nuevoC));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
        /// <summary>
        /// Este metodo valida los datos de autenticacion de un catador, estos son buscados
        /// y comparados con los datos existentes en el repositorio
        /// </summary>
        /// <param name="correoAdministrador"></param>
        /// <param name="contraseñaAdministrador"></param>
        /// <returns>Retorna True si correo y contraseña coinciden con los del repositorio, en 
        /// caso contraroio retorna false</returns>
        // GET: api/ApiAutenticar/
        [HttpGet]
        public bool validarCamposAdministrador(string correoAdministrador, string contraseñaAdministrador)
        {
            return buscarAdministrador(correoAdministrador, contraseñaAdministrador);
        }
        /// <summary>
        /// Este metodo verifica que el correo que es ingresado exista en el repositorio, tambien
        /// valida que la contraseña que se ingresa coincida con la contraseña almacenada. Para que 
        /// un catador pueda ser autenticado su estado debe ser igual a HABILITADO
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="contraseña"></param>
        /// <returns>Retorna un catador si la busqueda y comparacion de datos es exitosa, es decir,
        /// si el correo y la contraseña eran correctos, ademas que el estado fuera HABILITADO, 
        /// en otro caso retorna null</returns>
        private Catador buscarCatador(string correo, string contraseña)
        {
            CATADOR catadorDB = repositorio.consultarCatador(correo);

            if (catadorDB != null)
            {
                if (controladoraRCatador.VerificarMd5Hash(contraseña, catadorDB.CONTRASEÑA) && catadorDB.ESTADO.Equals("HABILITADO"))
                {
                    Catador catador = new Catador();
                    {
                        catador.cedula = catadorDB.CEDULA;
                        catador.codigo = catadorDB.CODIGO;
                        catador.correo = catadorDB.CORREO;
                        catador.nivelExp = catadorDB.NIVELEXP;
                        catador.nombre = catadorDB.NOMBRE;
                        catador.contrasena = catadorDB.CONTRASEÑA;
                    }
                    return catador;
                }
            }
            return null;
        }
        /// <summary>
        /// Este metodo permite realizar las pruebas a la autenticacion del catador
        /// </summary>
        /// <param name="correo">El correo del catador</param>
        /// <param name="contrasena">La contraseña del catador</param>
        /// <returns>Retorna un catador si los datos eran correctos, en caso contrario retorna null</returns>
        [Route("ApiAutenticar/NoEsServicio")]
        public Catador ValidarCatador(string correo, string contrasena)
        {
            return this.buscarCatador(correo, contrasena);
        }
        /// <summary>
        /// Este metodo se encarga de colsultar y verificar que los datos de autenticacion del 
        /// adimistrador sean correctos
        /// </summary>
        /// <param name="correo">Correo del administrador</param>
        /// <param name="contraseña">Contraseña del administrador</param>
        /// <returns>Retorna Verdadero si el correo y la contraseña son correctos, en 
        /// caso contrario retorna false</returns>
        private bool buscarAdministrador(string correo, string contraseña)
        {
            ADMINISTRADOR administradorDB = repositorio.consultarAdministrador(correo);
            if (administradorDB != null)
            {
                return controladoraRCatador.VerificarMd5Hash(contraseña, administradorDB.CONTRASEÑA);
            }
            return false;
        }
    }
}
