using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Reporte
    {
        string codigo { get; set; }
        string rutaGrafico { get; set; }
        LinkedList<string> observaciones { get; set; }

        public Reporte()
        {
            this.observaciones = new LinkedList<string>();
           

        }
    }
}