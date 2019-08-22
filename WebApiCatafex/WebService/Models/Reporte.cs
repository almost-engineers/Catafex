using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Reporte
    {
        public string codigo { get; set; }
        public string rutaGrafico { get; set; }
        public LinkedList<string> observaciones { get; set; }

        public Reporte()
        {
            this.observaciones = new LinkedList<string>();        
        }
    }
}