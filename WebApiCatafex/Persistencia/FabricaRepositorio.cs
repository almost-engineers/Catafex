using Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class FabricaRepositorio
    {
        public static Repositorio crearRepositorio() {
            return new EntityFramework();
        }
    }
}
