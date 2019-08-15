using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Cafe
    {
        public string codCafe { get; set; }
        public string procedencia { get; set; }
        public string origen { get; set; }
        public string nombre { get; set; }
        public int gradoMolienda { get; set; }
        public AtributosCafe atributosCafe { get; set; }
        public int puntoTueste { get; set; }
        public string tipoCafe { get; set; }
        public IList<string> datosCafe;

        public Cafe(string codCafe ,string procedencia,string origen,string nombre,string tipoCafe)
        {
            this.codCafe = codCafe;
            this.procedencia = procedencia;
            this.origen = origen;
            this.nombre = nombre;
            this.tipoCafe = tipoCafe;
            this.atributosCafe = new AtributosCafe();
            this.datosCafe = atributosCafe.datosCafe[this.tipoCafe];
        }

        public Cafe(string codCafe, string procedencia, string origen, string nombre, string tipoCafe,int puntoTueste,int gradoMolienda)
        {
            this.codCafe = codCafe;
            this.procedencia = procedencia;
            this.origen = origen;
            this.nombre = nombre;
            this.tipoCafe = tipoCafe;
            this.gradoMolienda = gradoMolienda;
            this.puntoTueste = puntoTueste;
            this.atributosCafe = new AtributosCafe();
            this.atributosCafe = new AtributosCafe();
            this.datosCafe = atributosCafe.datosCafe[this.tipoCafe];
        }
    }


}