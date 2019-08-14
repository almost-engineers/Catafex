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

        /// Se crea una lista de tipo Panel (Este tipo de dato es de Models de la capa de dominio), con el fin de poder retornar un objeto
        /// cuando es solicitado en los metodos consultarPaneles y consultarPanel, dado que un objeto EntityFramework no puede ser leido por 
        /// capas superiores
        private IList<Panel> paneles;
        /// Se crea una referencia de tipo EntityFramework con el cual se realiza el enlace de datos de la capa de persistencia
        private CatafexEntities db;
        public ApiGestionarPanelController()
        {
            this.paneles = new List<Panel>();
            this.db = new CatafexEntities();
          
        }

        // GET: api/ApiGestionarPanel/consultarPaneles
        /// <summary>
        ///     El metodo no recibe valores por parametro. El metodo tiene un ciclo el cual realiza una "consulta" a la base de datos por medio de EntityFramework
        ///     y retorna una lista de PANEL(tipo de dato de persistencia). A medida que el ciclo avanza a la lista paneles de la clase se le adiciona un panel.
        ///     El metodo es un metodo de tipo [HttpGet] -> Read
        /// <return>
        ///     Este metodo retorna una lista de todos los paneles de la base de datos, retorna un tipo de dato Panel de la capa de Dominio
        /// </return>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Panel> consultarPaneles()
        {
            foreach (PANEL panel in this.db.PANEL.ToList())
            {
                this.paneles.Add(new Panel()
                {
                    codigo = panel.CODPANEL,
                    codEvento = panel.EVENTO.CODEVENTO,
                    tipoCafe = panel.TIPOCAFE,
                    hora = panel.HORA
                }) ;
            }
            return this.paneles;
        }

        // GET: api/ApiGestionarPanel/5
        /// <summary>
        ///     Se recibe por parametro un codigo, el cual se consulta en la base de datos por medio de LINQ. Esta consulta retorna un PANEL, el cual
        ///     es "transformado" en un objeto del tipo Panel para poder ser retornado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet]
        public Panel consultarPanel(string codigo)
        {      
            PANEL panel= this.db.PANEL.FirstOrDefault(x => x.CODPANEL.Equals(codigo));
            if (panel!=null)
            {
                return new Panel()
                {
                    codigo = panel.CODPANEL,
                    codEvento = panel.EVENTO.CODEVENTO,
                    tipoCafe = panel.TIPOCAFE,
                    hora = panel.HORA
                };
            }
            return null;
        }

        // POST: api/ApiGestionarPanel
        /// <summary>
        ///     Este metodo recibe por parametro todos los atributos necesarios para la creacion de un panel. Por medio del metodo Add se registra un nuevo 
        ///     PANEL en la base de datos, y se almacenan los cambios. Retorna true si no se produce ninguna excepcion de lo contrario retorna false
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="codEvento"></param>
        /// <param name="tipoCafe"></param>
        /// <param name="hora"></param>
        /// <returns></returns>
        [HttpPost]
        public bool ingresarPanel(string codigo, string codEvento, string tipoCafe, string hora)
        {
            try
            {
                this.db.PANEL.Add(new PANEL()
                {
                    CODPANEL = codigo,
                    CODEVENTO = "EV-01",
                    TIPOCAFE = tipoCafe,
                    HORA = TimeSpan.Parse("21:00")

                });
                this.db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en metodo insertar: " + e);
                return false;
            }
        }

        // PUT: api/ApiGestionarPanel/5
        /// <summary>
        ///     Recibe por parametros todos los atributos de Panel, por medio de un ciclo se busca que panel coincide con el codigo ingresado,
        ///     cuando un codigo coincide, todos los valores de este son actualizados por los parametros de entrada. Se retorna true si no sucede 
        ///     ninguna excepcion.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="codEvento"></param>
        /// <param name="tipoCafe"></param>
        /// <param name="hora"></param>
        /// <returns></returns>
        [HttpPut]
        public bool actualizarPanel(string codigo, string codEvento, string tipoCafe, string hora)
        {
            try
            {
               
                foreach (PANEL panel in this.db.PANEL.ToList())
                {
                    if (panel.CODPANEL.Equals(codigo))
                    {
                        panel.CODEVENTO = codEvento;
                        panel.TIPOCAFE = tipoCafe;
                        panel.HORA = TimeSpan.Parse(hora);
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // DELETE: api/ApiGestionarPanel/5
        /// <summary>
        ///     Se recibe como parametro un codigo de Panel, este es buscado por medio de un ciclo en la base de datos, cuando el codigo coincide
        ///     se elimina este panel de la base de datos, se guardan los cambios y se retorna true si no sucede ninguna excepcion.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool eliminarPanel(string codigo)
        {
            try
            {
                foreach (PANEL panel in this.db.PANEL.ToList())
                {
                    if (panel.CODPANEL.Equals(codigo))
                    {
                        this.db.PANEL.Remove(panel);
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
