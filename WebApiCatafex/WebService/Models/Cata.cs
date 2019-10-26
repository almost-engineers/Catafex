using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Cata
    {

        public string codCata { get; set; }
        public int vezCatada { get; set; }
        public int rancidez { get; set; }
        public int dulce { get; set; }
        public int acidez { get; set; }
        public int cuerpo { get; set; }
        public int aroma { get; set; }
        public int amargo { get; set; }
        public int impresionGlobal { get; set; }
        public int fragancia { get; set; }
        public int saborResidual { get; set; }
        public string observaciones { get; set; }

        public Cata(string CODIGO, int VEZCATADA, int RANCIDEZ, int DULCE, int ACIDEZ, int AROMA, int AMARGO, int FRAGANCIA, int SABORESIDUAL,
            int CUERPO, int IMPRESIONGLOBAL, string OBSERVACIONES)
        {
            this.vezCatada = VEZCATADA;
            this.codCata = CODIGO;
            this.rancidez = RANCIDEZ;
            this.dulce = DULCE;
            this.acidez = ACIDEZ;
            this.aroma = AROMA;
            this.amargo = AMARGO;
            this.fragancia = FRAGANCIA;
            this.saborResidual = SABORESIDUAL;
            this.cuerpo = CUERPO;
            this.impresionGlobal = IMPRESIONGLOBAL;
            this.observaciones = OBSERVACIONES;

        }

    }

}