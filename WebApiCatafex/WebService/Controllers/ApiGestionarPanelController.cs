using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;
using Persistencia;
using Persistencia.Entity;

namespace WebService.Controllers
{
    public class ApiGestionarPanelController : ApiController
    {

        /// Se crea una variable tipo Repositorio, que retorna ya sea un EntityFramework o una lista
        private Repositorio repositorio;
        public ApiGestionarPanelController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        // GET: api/ApiGestionarPanel/consultarPaneles
       /// <summary>
       ///      Este metodo hace uso del metodo convertirPANEL para trasformar un dato de tipo PANEL proveniente de Entity a un
       ///      tipo Panel de la capa de Dominio. Envia como parametro para convertir el resultado de llamar a consultar paneles
       ///      implementado en alguno de los dos repositorios
       /// </summary>
       /// <returns>Retorna una lista de paneles de la capa de Dominio</returns>
        [HttpGet]
        public IEnumerable<Panel> consultarPaneles()
        {           
            return this.convertirPANEL(repositorio.consultarPaneles());
        }
        /// <summary>
        /// Este metodo recibe como parametro una lista de PANELES e instancia, una variable de tipo List (IList es abstracto),
        /// mientras se recorre el ciclo, se añade a la lista de Paneles (capa Dominio) para finalmente esta ser retornada, con todos
        /// los datos de Paneles de la base de datos
        /// </summary>
        /// <param name="panelesDB"></param>
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

        // GET: api/ApiGestionarPanel/5
   
        [HttpGet]
        public Panel consultarPanel(string codigo)
        {
            return this.convertirPANEL(codigo);
        }

        private Panel convertirPANEL(string codigo)
        {
            PANEL panelDB = repositorio.consultarPanel(codigo);
            Panel panel = new Panel();
            {
                panel.codigo = panelDB.CODPANEL;
                panel.codEvento = panelDB.CODEVENTO;
                panel.tipoCafe = panelDB.TIPOCAFE;
                panel.hora = panelDB.HORA;
            }           
            return panel;
        }

        // POST: api/ApiGestionarPanel
       
        [HttpPost]
        public bool ingresarPanel(string codEvento, string tipoCafe, string hora)
        {
            return repositorio.insertarPanel(codEvento, tipoCafe, TimeSpan.Parse(hora));
        }

        [HttpPut]
        public bool actualizarPanel(string codigo, string codEvento, string tipoCafe, string hora)
        {
            return repositorio.actualizarPanel(codigo, codEvento, tipoCafe, TimeSpan.Parse(hora));
        }

       
        [HttpDelete]
        public bool eliminarPanel(string codigo)
        {
            return repositorio.eliminarPanel(codigo);
        }
    }
}
