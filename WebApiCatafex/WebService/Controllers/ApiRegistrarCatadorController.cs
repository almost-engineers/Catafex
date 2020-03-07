using Newtonsoft.Json;
using Persistencia;
using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using WebService.Models;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ApiRegistrarCatadorController : ApiController
    {

        private Repositorio repositorio;
        public ApiRegistrarCatadorController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        [HttpGet]
        [Route("api/ApiRegistrarCatador/catadorHabilitado")]
        public HttpResponseMessage catadorHabilitado(string codCatador)
        {
            CATADOR catador = this.repositorio.catadorHabilitado(codCatador);
            if (catador != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(this.convertirCATADOR(catador.CEDULA)));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        // POST: api/ApiRegistrarCatador
        /// <summary>
        /// El metodo insertar Catador recibe como parametros todos los datos necesarios para crear un catador, inlcuido el codigo dado que este es 
        /// unico para cada catador, y proviene de su documento oficial de catador para los Experimentados, o el codigo del estudiante para los
        /// SemiExperimentados. Lo primero que se realiza es una validacion de que la cedula no se encuentre registrada, esto con el fin de identificar
        /// que dicho catador no se encuentre registrado
        /// </summary>
        /// <param name="catador"></param>
        /// <returns>En este metodo existen tres puntos de retorno, uno de ellos se da en el momento en el que la validacion de la cedula da como
        /// resultado true, esto quiere decir que la cedula ya existe por ende ya esta registrado el catador. Los otros dos se producen por medio
        /// de una excepcion. En caso de no ser exitosa la insercion, la excepcion retorna false
        /// </returns>
        [HttpPost]
        ///[Route("api/RegistrarCatador")]
        public HttpResponseMessage insertarCatador(Catador catador)
        {
            var result = this.validarCedula(catador.cedula);
            if (result.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                try
                {
                    if (repositorio.insertarCatador(catador.nombre, catador.cedula, catador.codigo, catador.correo, this.getMD5Hash(catador.contrasena), catador.nivelExp))
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    return new HttpResponseMessage(HttpStatusCode.BadGateway);
                }
                catch (Exception)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        /// <summary>
        /// Este metodo recibe por parametro la cedula del catador para ser consultada en la base de datos
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns>Retorna Verdadero o Falso, esto depende de si ya se encuentra registrado el catador, de esto ser asi
        /// se retorna true, de lo contrario retorna false
        /// </returns>
        [HttpGet]
        public HttpResponseMessage validarCedula(string cedula)
        {
            Catador catador = convertirCATADOR(cedula);
            if (catador == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(catador));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.BadGateway);
                }
            }
        }
        /// <summary>
        /// Este metodo se encarga de eliminar la informacion personal de un catador a patir de su cedula, 
        /// </summary>
        /// <param name="cedula">Cedula del catador</param>
        /// <returns>Retorna OK si la operacion de eliminacion fue exitosa, en caso contrario retorna BadGateway </returns>
        [HttpDelete]
        public HttpResponseMessage eliminarCatador(string cedula)
        {
            try
            {
                if (this.repositorio.eliminarCatador(cedula))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
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
        /// Este metodo se encarga de actualizar los datos pertenecientes a un catador, 
        /// </summary>
        /// <param name="catador">Cedula del catador</param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage actualizarCatador(Catador catador)
        {
            try
            {
                string nContrasena;
                if (this.VerificarMd5Hash(this.convertirCATADOR(catador.cedula).contrasena, catador.contrasena))
                {
                    nContrasena = catador.contrasena;
                }
                else
                {
                    nContrasena = this.getMD5Hash(catador.contrasena);
                }
                if (this.repositorio.actualizarCatador(catador.nombre, catador.cedula, catador.correo, nContrasena))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
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
        /// Este metodo se encarga de convertir un CATADOR (objeto de base de datos) en un 
        /// Catador (Models), a partir de la cedula se busca el catador y se transforma la informacion
        /// </summary>
        /// <param name="cedula">Cedula del catador</param>
        /// <returns>Retorna un Catador (objeto de Models) si la cedula ingresada es correcta y 
        /// existe en el repositorio, en caso contrario retorna null</returns>
        private Catador convertirCATADOR(string cedula)
        {
            CATADOR catadorDB = repositorio.buscarCedulaCatador(cedula);
            if (catadorDB != null)
            {
                Catador catador = new Catador();
                {
                    catador.codigo = catadorDB.CODIGO;
                    catador.cedula = catadorDB.CEDULA;
                    catador.contrasena = catadorDB.CONTRASEÑA;
                    catador.correo = catadorDB.CORREO;
                    catador.nivelExp = catadorDB.NIVELEXP;
                    catador.nombre = catadorDB.NOMBRE;
                    catador.estado = catadorDB.ESTADO;
                }
                return catador;
            }
            return null;
        }

        /// <summary>
        /// Este metodo se encarga de obtener el hash md5 de una cadena de texto, en este caso la cadena de entrada
        /// es la contraseña suministrada por el usuario. El texto de entrada es transformado en bytes de informacion 
        /// por medio de metodos propios de las librerias de c#, a partir de esto se recorre cada byte de informacion
        /// y se retorna una cadena de texto
        /// </summary>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        private string getMD5Hash(string contraseña)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] datos = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder sBuilder = new StringBuilder();
                foreach (byte b in datos)
                {
                    //Le da un formato hexadecimal a cada byte de informacion, ademas de transformalo en string
                    sBuilder.Append(b.ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        //Metodo para Julian y Jhonathan
        /// <summary>
        /// Este metodo se encarga de comparar el hash md5 contra la contraseña en formato plano. Primero la contraseña ingresada
        /// es convertida a md5, luego es comparada con el hash ya existente (Debe ser traido de la base de datos), por medio de una 
        /// funcion string se comparan los dos hash, si son iguales (en este caso la comparacion debe retornar 0) retorna verdadero de
        /// lo contrario retorna falso
        /// </summary>
        /// <param name="contraseña"></param>
        /// <param name="hash"></param>
        /// <returns>Retorna Falso o Verdadero, dependiendo de la comparacion</returns>
        protected internal bool VerificarMd5Hash(string contraseña, string hash)
        {
            const int RESPUESTACOMPARER = 0;
            string hashContraseña = getMD5Hash(contraseña);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashContraseña, hash) == RESPUESTACOMPARER;
        }

        [HttpGet]
        public IEnumerable<Catador> consultarCatadores()
        {
            return this.convertirCATADORES(repositorio.consultarCatadores());
        }

        [HttpGet]
        [Route("api/RegistrarCatador/obtenerInhabilitados")]
        public HttpResponseMessage getCatadoresInhabilitados()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(this.convertirCATADORES(this.repositorio.getCatadoresInhabilitados())));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/RegistrarCatador/obtenerHabilitados")]
        public HttpResponseMessage getCatadoresHabilitados()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(this.convertirCATADORES(this.repositorio.getCatadoresHabilitados())));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpPut]
        [Route("api/RegistrarCatador/cambiarEstado")]
        public HttpResponseMessage habilitarCatador(string codCatador)
        {
            if (this.repositorio.habilitarCatador(codCatador))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        private IEnumerable<Catador> convertirCATADORES(IList<CATADOR> catadoresDB)
        {
            IList<Catador> catadores = new List<Catador>();
            foreach (CATADOR catador in catadoresDB)
            {
                Catador ctador = new Catador(catador.NOMBRE, catador.CEDULA, catador.CORREO, catador.CONTRASEÑA, catador.NIVELEXP, catador.CODIGO);
                ctador.estado = catador.ESTADO;
                catadores.Add(ctador);

            }
            return catadores;
        }
    }


}
