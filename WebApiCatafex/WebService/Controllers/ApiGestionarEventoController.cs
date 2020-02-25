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
        /// <summary>
        /// Este metodo se encarga de consultar y listar todos los eventos
        /// </summary>
        /// <returns>Una lista con todos los eventos</returns>
        [HttpGet]
        public IEnumerable<Evento> consultarEventos()
        {
            return this.convertirEVENTO(repositorio.consultarEventos());
        }
        /// <summary>
        /// Este metodo se encarga de transformar el tipo de EVENTO en Evento de este modo, el 
        /// servicio puede devolver una lista de datos que puedan ser interpretados 
        /// posteriormente, por la parte que solicita el servicio REST
        /// </summary>
        /// <param name="eventosDB">Lista de eventos de tipo EVENTO (base de datos)</param>
        /// <returns>Una lista de eventos de tipo Evento (Models)</returns>
        private IList<Evento> convertirEVENTO(IList<EVENTO> eventosDB)
        {
            IList<Evento> eventos = new List<Evento>();
            foreach (EVENTO evento in eventosDB)
            {
                eventos.Add(new Evento(evento.CODEVENTO, evento.NOMBRE, evento.FECHA));
              
            }
            return eventos;
        }
        /// <summary>
        /// Este metodo se encarga de consultar y listar la informacion correspondiente 
        /// a un Evento en especifico a partir de su codigo
        /// </summary>
        /// <param name="codigo">Codigo del evento</param>
        /// <returns>Retorna un Evento si el codigo ingresado es valido, en caso contrario
        /// retorna null</returns>
        [HttpGet]
        public Evento consultarEvento(string codigo)
        {
            return this.convertirEVENTO(codigo);
        }
        /// <summary>
        /// Este metodo se encarga de consultar los datos de un evento a partir de su codigo,
        /// de este modo se obtiene un EVENTO (dato de base de datos)  que sera transformado en un 
        /// Evento (dato correspondiente a Models)
        /// </summary>
        /// <param name="codigo">Codigo del evento</param>
        /// <returns>Un Evento de tipo Evento (Models)</returns>
        private Evento convertirEVENTO(string codigo)
        {
            EVENTO eventoDB = repositorio.consultarEvento(codigo);
            Evento evento = new Evento(eventoDB.CODEVENTO, eventoDB.NOMBRE, eventoDB.FECHA);
            return evento;
        }
        /// <summary>
        /// Este metodo se encarga de adicionar un nuevo evento al repositorio
        /// </summary>
        /// <param name="nombre">Nombre del evento</param>
        /// <param name="fecha">Fecha del evento</param>
        /// <returns>Retorna true si se realizo de manera exitosa la insercion en el
        /// reositorio, en caso contrario retorna false</returns>
        [HttpPost]
        public bool ingresarEvento(string nombre, string fecha)
        {
            return repositorio.insertarEvento(nombre, Convert.ToDateTime(fecha));
        }
        /// <summary>
        /// Este metodo se encarga de actualizar los datos de un evento, a partir de su codigo
        /// </summary>
        /// <param name="codigo">Codigo del evento</param>
        /// <param name="nombre">Nombre del evento</param>
        /// <param name="fecha">Fecha del evento</param>
        /// <returns>Retorna true si el codigo a eliminar es correcto en caso contrario retorna false</returns>
        [HttpPut]
        public bool actualizarEvento(string codigo, string nombre, string fecha)
        {
            return repositorio.actualizarEvento(codigo, nombre, Convert.ToDateTime(fecha));
        }
        /// <summary>
        /// Este metodo se encarga de eliminar un evento del repositorio a partir del codigo de este
        /// </summary>
        /// <param name="codigo">Codigo del evento</param>
        /// <returns>Retorna true si el codigo a eliminar es correcto en caso contrario retorna false</returns>
        [HttpDelete]
        public bool eliminarEvento(string codigo)
        {
            return repositorio.eliminarEvento(codigo);
        }

    }
}
