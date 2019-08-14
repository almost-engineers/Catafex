using Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public static class FabricaRepositorio
    {   /// <summary>
        /// Version 1.0
        /// El metodo por el momento y para facilidad solo retorna un objeto de la clase EntityFramework, el anterior es diferente de la carpeta Entity
        /// El metodo tiene como objetivo leer un archivo de configuracion y dependiendo de los valores leidos retornar como repositorio la base de datos
        /// o un objeto de la clase Lista
        /// </summary>
        /// <returns></returns>
        public static Repositorio crearRepositorio()
        {
            return new EntityFramework();
        }
    }
}
