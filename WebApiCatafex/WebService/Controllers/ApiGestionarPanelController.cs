using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;
using Persistencia;
using Persistencia.Entity;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ApiGestionarPanelController : ApiController
    {

        /// Se crea una variable tipo Repositorio, que retorna ya sea un EntityFramework o una lista
        private Repositorio repositorio;
        private ApiGestionarCafeController gestionarCafe = new ApiGestionarCafeController();
        public ApiGestionarPanelController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }
        [HttpGet]
        [Route("api/Panel/panelPerteneceEvento")]
        public bool panelPerteneEvento(string codPanel, string codEvento)
        {
            return this.repositorio.pertenecePanel(codPanel, codEvento);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Panel/cafesTipoCafePanel")]
        public HttpResponseMessage obtenerCafesTipoCafePanel(string codPanel)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(this.gestionarCafe.convertirCAFE(this.repositorio.obtenerCafesMismoTipoPanel(codPanel))));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
           
        }
        // GET: api/ApiGestionarPanel/consultarPaneles
       /// <summary>
       ///      Este metodo hace uso del metodo convertirPANEL para trasformar un dato de tipo PANEL proveniente de Entity a un
       ///      tipo Panel de la capa de Dominio. Envia como parametro para convertir el resultado de llamar a consultar paneles
       ///      implementado en alguno de los dos repositorios
       /// </summary>
       /// <returns>Retorna una lista de paneles de la capa de Dominio</returns>
        [HttpGet]
        public HttpResponseMessage consultarPaneles()
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(this.convertirPANEL(repositorio.consultarPaneles())));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
            
        }
        /// <summary>
        /// Este metodo recibe como parametro una lista de PANELES e instancia, una variable de tipo List (IList es abstracto),
        /// mientras se recorre el ciclo, se añade a la lista de Paneles (capa Dominio) para finalmente esta ser retornada, con todos
        /// los datos de Paneles de la base de datos
        /// </summary>
        /// <param name="panelesDB">Lista de paneles de tipo PANEL (base de datos)</param>
        /// <returns>Una lista de Paneles</returns>
        private IList<Panel> convertirPANEL(IList<PANEL> panelesDB)
        {
            IList<Panel> paneles = new List<Panel>();
            foreach (PANEL panel in panelesDB)
            {
                paneles.Add(new Panel()
                {
                    codigo = panel.CODPANEL,
                    codEvento = panel.CODEVENTO,
                    tipoCafe = panel.TIPOCAFE,
                    hora = panel.HORA
                });
            }
            return paneles;
        }
        [HttpGet]
        [Route("api/Panel/obtenerPanelesPorEvento")]
        public HttpResponseMessage obtenerPanelesporEvento(string codEvento)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(this.convertirPANEL(repositorio.consultarPanelesPorEvento(codEvento))));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        // GET: api/ApiGestionarPanel/5
        /// <summary>
        /// Este metodo recibe como parametro el codigo de un panel. Este parametro pasa a ser el parametro de entrada del metodo convertirPANEL
        /// </summary>
        /// <param name="codigo">Codigo del panel</param>
        /// <returns>Un objeto de tipo Panel con todos los atributos del panel que coincide con el codigo ingresado por parametro</returns>
        [HttpGet]
        public HttpResponseMessage consultarPanel(string codigo)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(this.convertirPANEL(codigo)));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
            
        }
        /// <summary>
        /// Este metodo trae por instancia de la clase repositorio un PANEL. Este panel se "transforma" a un objeto de tipo Panel, esto se realiza de
        /// manera manual, asignando cada atributo
        /// </summary>
        /// <param name="codigo">Codigo del panel</param>
        /// <returns>Se retorna un objeto de tipo panel</returns>
        private Panel convertirPANEL(string codigo)
        {
            PANEL panelDB = repositorio.consultarPanel(codigo);
            if(panelDB != null)
            {
                Panel panel = new Panel();
                {
                    panel.codigo = panelDB.CODPANEL;
                    panel.codEvento = panelDB.CODEVENTO;
                    panel.tipoCafe = panelDB.TIPOCAFE;
                    panel.hora = panelDB.HORA;
                }
                return panel;
            }           
            return null;
        }

        // POST: api/ApiGestionarPanel
       /// <summary>
       /// Este metodo recibe por parametro todos los valores necesario para crear un panel, estos atributos
       /// son nuevamente ingresados por parametro a el metodo insertar panel de repositorio, y retorna un valor
       /// booleando dependiendo del estado de insercion del dato
       /// </summary>
       /// <param name="codEvento">Codigo del evento al cual pertenece el panel</param>
       /// <param name="tipoCafe">Ej : Verde, Soluble</param>
       /// <param name="hora">Hora del panel</param>
       /// <returns>Verdadero o Falso, dependiendo de si fue o no exitosa la operacion de insercion</returns>
        [HttpPost]
        public bool ingresarPanel(string codEvento, string tipoCafe, string hora)
        {
            return repositorio.insertarPanel(codEvento, tipoCafe, TimeSpan.Parse(hora));
        }
        /// <summary>
        /// Este metodo recibe por parametro el codigo de un Panel, y retorna el valor booleando de ejecutar el metodo
        /// actualizar panel del repositorio, este a su vez recibe como parametro el codigo del Panel
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="codEvento"></param>
        /// <param name="tipoCafe"></param>
        /// <param name="hora"></param>
        /// <returns>Retorna verdadero o falso dependiendo de si fue exitosa o no la actualizacion del Panel de la
        /// base de datos</returns>
        [HttpPut]
        public bool actualizarPanel(string codigo, string codEvento, string tipoCafe, string hora)
        {
            return repositorio.actualizarPanel(codigo, codEvento, tipoCafe, TimeSpan.Parse(hora));
        }

        /// <summary>
        /// Este metodo recibe por parametro el codigo de un Panel, y retorna el valor booleando de ejecutar el metodo
        /// eliminar panel del repositorio, este a su vez recibe como parametro el codigo del Panel
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>Retorna verdadero o falso dependiendo de si fue exitosa o no la eliminacion del Panel de la
        /// base de datos</returns>
        [HttpDelete]
        public bool eliminarPanel(string codigo)
        {
            return repositorio.eliminarPanel(codigo);
        }
    }
}
