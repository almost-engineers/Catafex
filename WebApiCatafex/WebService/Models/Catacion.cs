using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Catacion
    {
        public string codCatacion { get; set; }
        public string codPanel { get; set; }
        public string codCatador { get; set; }
        public string codCafe { get; set; }
        public int cantidad { get; set; }
    }
}