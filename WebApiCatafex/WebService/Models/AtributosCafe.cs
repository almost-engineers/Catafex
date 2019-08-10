using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class AtributosCafe
    {
        public string tipoCafe { get; set; }
        public LinkedList<string> datos { get; set; }

        public AtributosCafe()
        {
            this.datos = new LinkedList<string>();
        }
    }
}