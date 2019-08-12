using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Panel
    {
        public string codigo { get; set; }
        public string tipoCafe { get; set; }
        public DateTime hora { get; set; }
        public bool terminado { get; set; }
        public Reporte reporte { get; set; }

       public Panel()
        {

        }

        public Panel(string codigo, string tipoCafe, DateTime hora)
        {
            this.codigo = codigo;
            this.tipoCafe = tipoCafe;
            this.hora = hora;
            this.terminado = false;
            this.reporte = new Reporte();

        }

    }
}