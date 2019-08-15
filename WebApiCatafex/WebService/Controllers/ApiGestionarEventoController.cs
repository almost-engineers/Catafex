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
    public class ApiGestionarEventoController : ApiController
    {
        Repositorio repositorio;

        public ApiGestionarEventoController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }

        [HttpGet]
        public IEnumerable<Evento> consultarEventos()
        {
            return this.convertirEVENTO(repositorio.consultarEventos());
        }

        private IList<Evento> convertirEVENTO(IList<EVENTO> eventosDB)
        {
            IList<Evento> eventos = new List<Evento>();
            foreach (EVENTO evento in eventosDB)
            {
                eventos.Add(new Evento(evento.CODEVENTO, evento.NOMBRE, evento.FECHA));
              
            }
            return eventos;
        }
        [HttpGet]
        public Evento consultarEvento(string codigo)
        {
            return this.convertirEVENTO(codigo);
        }

        private Evento convertirEVENTO(string codigo)
        {
            EVENTO eventoDB = repositorio.consultarEvento(codigo);
            Evento evento = new Evento(eventoDB.CODEVENTO, eventoDB.NOMBRE, eventoDB.FECHA);
           
            return evento;
        }


        [HttpPost]
        public bool ingresarEvento(string nombre, string fecha)
        {
            return repositorio.insertarEvento(nombre, Convert.ToDateTime(fecha));
        }

        [HttpPut]
        public bool actualizarEvento(string codigo, string nombre, string fecha)
        {
            return repositorio.actualizarEvento(codigo, nombre, Convert.ToDateTime(fecha));
        }
        [HttpDelete]
        public bool eliminarEvento(string codigo)
        {
            return repositorio.eliminarEvento(codigo);
        }

    }
}
