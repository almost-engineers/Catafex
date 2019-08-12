using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class ApiGestionarPanelController : ApiController
    {

        private IList<Panel> paneles;
        public ApiGestionarPanelController()
        {
            this.paneles = new List<Panel>();
            this.llenarLista();
        }
        private void llenarLista()
        {
            this.paneles.Add(new Panel("PA-01", "Verde", DateTime.Now));
            this.paneles.Add(new Panel("PA-02", "Verde", DateTime.Now));
        }
        // GET: api/ApiGestionarPanel/consultarPaneles

        [HttpGet]
        public IEnumerable<Panel> consultarPaneles()
        {
            return this.paneles;
        }

        // GET: api/ApiGestionarPanel/5
        public Panel consultarPanel(string codigo)
        {
            foreach(Panel panel in this.paneles)
            {
                if (panel.codigo.Equals(codigo))
                {
                    return panel;
                }
            }
            return null;
        }

        // POST: api/ApiGestionarPanel
        public bool Post(string codigo, string tipoCafe, string hora)
        {
            try
            {
                this.paneles.Add(new Panel(codigo, tipoCafe, Convert.ToDateTime(hora)));
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        
        // PUT: api/ApiGestionarPanel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiGestionarPanel/5
        public void Delete(int id)
        {
        }
    }
}
