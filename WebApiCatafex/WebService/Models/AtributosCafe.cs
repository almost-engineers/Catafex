using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class AtributosCafe
    {
        //public string tipoCafe { get; set; }
        //public LinkedList<string> datos { get; set; }
        private Dictionary<string, IList<string>> datosCafe;

        public AtributosCafe()
        {
            IList<string> datosVerde = new LinkedList<string>();
            IList<string> datosEmpaque = new LinkedList<string>();
            IList<string> datosSoluble = new LinkedList<string>();
            IList<string> datosExtractoCafe = new LinkedList<string>();
            datosCafe.Add("verde", datosVerde);
            datosCafe.Add("empaque", datosEmpaque);
            datosCafe.Add("soluble", datosSoluble);
            datosCafe.Add("extractoCafe", datosExtractoCafe);
        }

    }
}