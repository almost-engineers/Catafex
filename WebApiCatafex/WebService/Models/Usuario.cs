using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
   public abstract class Usuario
    {
         public string cedula { get; set; }
         public string correo { get; set; }
         public string nombre { get; set; }
         public string contrasena { get; set; }

        public Usuario (string nombre, string cedula, string correo, string contrasena)
        {
            this.nombre = nombre;
            this.cedula = cedula;
            this.correo = correo;
            this.contrasena = contrasena;
        }

    }
}