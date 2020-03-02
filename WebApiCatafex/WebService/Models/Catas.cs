using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Catas
    {
        public string codCafe { get; private set; }
        public int vezCatada { get; private set; }
        public string hora { get; private set; }
        public string fecha { get; private set; }
        public string tipoCafe { get; private set; }

        public string atributos { get; private set; }

        public string valoresDefecto { get; private set; }
        


        public Catas(string CodCafe, int vezCatada, string hora, string fecha, string tipoCafe,string atributos, string valoresDefecto)
        {
            this.codCafe = CodCafe;
            this.vezCatada = vezCatada;
            this.hora = hora;
            this.fecha = fecha;
            this.tipoCafe = tipoCafe;
            this.atributos = atributos;
            this.valoresDefecto = valoresDefecto;
        }
    }
}