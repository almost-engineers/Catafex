using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Catador : Usuario
    {
        public string codigo { get; set; }
        public string nivelExp { get; set; }
        public string estado { get; set; }

        public Catador() : base() { }
        public Catador(string nombre, string cedula, string correo, string contrasena, string nivelExp, string codigo) : base(nombre, cedula, correo, contrasena)
        {
            this.codigo = codigo;
            this.nivelExp = nivelExp;
        }


    }
}