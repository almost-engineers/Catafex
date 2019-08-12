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
        [HttpGet]
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
        [HttpPost]
        public bool Post(string codigo, string tipoCafe, string hora)
        {
            try
            {
                Panel panel = new Panel(codigo, tipoCafe, DateTime.Now);
                this.paneles.Add(panel);
                Console.Write(this.paneles.Count());
                Console.ReadKey();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
        
        // PUT: api/ApiGestionarPanel/5
        [HttpPut]
        public bool actualizarPanel(string codigo, string tipoCafe, string hora)
        {
            try
            {
                foreach(Panel panel in this.paneles)
                {
                    if (panel.codigo.Equals(codigo))
                    {
                        panel.tipoCafe = tipoCafe;
                        panel.hora = Convert.ToDateTime(hora);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // DELETE: api/ApiGestionarPanel/5
        [HttpDelete]
        public bool eliminarPanel(string codigo)
        {
            try
            {
                foreach(Panel panel in this.paneles)
                {
                    if (panel.codigo.Equals(codigo))
                    {
                        this.paneles.Remove(panel);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
