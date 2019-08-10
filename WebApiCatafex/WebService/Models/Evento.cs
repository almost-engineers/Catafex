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
        public LinkedList<Cafe> cafes { get; set; }
        public LinkedList<Panel> paneles { get; set; }
        public Evento()
        {
            this.cafes = new LinkedList<Cafe>();
            this.paneles = new LinkedList<Panel>();
        }
    }
}