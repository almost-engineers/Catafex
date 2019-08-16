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

        public Cafe()
        {
            this.atributosCafe = new AtributosCafe();
        }
    }
}