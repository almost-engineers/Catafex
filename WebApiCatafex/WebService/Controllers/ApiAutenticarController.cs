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

namespace WebService.Controllers
{
    public class ApiAutenticarController : ApiController
    {
        private Repositorio repositorio;
        private ApiRegistrarCatadorController controladoraRCatador;
        public ApiAutenticarController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
            controladoraRCatador = new ApiRegistrarCatadorController();
        }

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
        // GET: api/ApiAutenticar/
        [HttpGet]
        public bool validarCamposAdministrador(string correoAdministrador, string contraseñaAdministrador)
        {
            return buscarAdministrador(correoAdministrador, contraseñaAdministrador);
        }
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
        [Route("ApiAutenticar/NoEsServicio")]
        public Catador ValidarCatador(string correo, string contrasena)
        {
            return this.buscarCatador(correo, contrasena);
        }



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
