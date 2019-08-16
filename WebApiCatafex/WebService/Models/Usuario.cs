using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
   public abstract class Usuario
    {
         string cedula { get; set; }
         string correo { get; set; }
         string nombre { get; set; }
         string contraseña { get; set; }

    }
}