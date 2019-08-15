using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Evento
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
     
      
        public Evento(string codigo, string nombre, DateTime fecha ) {
            this.codigo = codigo;
            this.nombre = nombre;
            this.fecha = fecha;
        }
    }
}