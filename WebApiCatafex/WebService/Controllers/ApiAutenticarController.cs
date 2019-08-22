using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia.Entity;


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
        [HttpGet]
        public bool validarCamposCatador(string correoCatador, string contraseñaCatador)
        {
            return buscarCatador(correoCatador,contraseñaCatador);
        }
        // GET: api/ApiAutenticar/
        [HttpGet]
        public bool validarCamposAdministrador(string correoAdministrador, string contraseñaAdministrador)
        {
            return buscarAdministrador(correoAdministrador, contraseñaAdministrador);
        }
        private bool buscarCatador(string correo,string contraseña)
        {
            CATADOR catadorDB = repositorio.consultarCatador(correo);
            
            if(catadorDB != null)
            {
                return controladoraRCatador.VerificarMd5Hash(contraseña, catadorDB.CONTRASEÑA);
            }
            
            return false;
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
