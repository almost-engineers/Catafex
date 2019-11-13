using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Administrador : Usuario
    {
        public Administrador(string nombre, string cedula, string correo, string contraseña) : base(nombre, cedula, correo, contraseña) { }
    }
}