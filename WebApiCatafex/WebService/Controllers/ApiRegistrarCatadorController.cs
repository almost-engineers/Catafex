using Newtonsoft.Json;
using Persistencia;
using Persistencia.Entity;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class ApiRegistrarCatadorController : ApiController
    {
        
        private Repositorio repositorio;
        public ApiRegistrarCatadorController()
        {
           this.repositorio = FabricaRepositorio.crearRepositorio();
        }
        // POST: api/ApiRegistrarCatador
        /// <summary>
        /// El metodo insertar Catador recibe como parametros todos los datos necesarios para crear un catador, inlcuido el codigo dado que este es 
        /// unico para cada catador, y proviene de su documento oficial de catador para los Experimentados, o el codigo del estudiante para los
        /// SemiExperimentados. Lo primero que se realiza es una validacion de que la cedula no se encuentre registrada, esto con el fin de identificar
        /// que dicho catador no se encuentre registrado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="cedula"></param>
        /// <param name="codigo"></param>
        /// <param name="correo"></param>
        /// <param name="contraseña"></param>
        /// <param name="nivelExp"></param>
        /// <returns>En este metodo existen tres puntos de retorno, uno de ellos se da en el momento en el que la validacion de la cedula da como
        /// resultado true, esto quiere decir que la cedula ya existe por ende ya esta registrado el catador. Los otros dos se producen por medio
        /// de una excepcion. En caso de no ser exitosa la insercion, la excepcion retorna false
        /// </returns>
        [HttpPost]
        public HttpResponseMessage insertarCatador(Catador catador)
        {
            var result = this.validarCedula(catador.cedula);
            if (result.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                try
                {   
                    repositorio.insertarCatador(catador.nombre, catador.cedula, catador.codigo, catador.correo, this.getMD5Hash(catador.contrasena), catador.nivelExp);
                    return new HttpResponseMessage(HttpStatusCode.OK);
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
            Catador catador= convertirCATADOR(cedula);
            if(catador == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else{
                try {
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

        [HttpDelete]
        public HttpResponseMessage eliminarCatador(string cedula)
        {
            try
            {
                this.repositorio.eliminarCatador(cedula);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

        [HttpPut]
        public HttpResponseMessage actualizarCatador(Catador catador)
        {
            try
            {
                this.repositorio.actualizarCatador(catador.nombre,catador.cedula,catador.correo, this.getMD5Hash(catador.contrasena));
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

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
    }
}
