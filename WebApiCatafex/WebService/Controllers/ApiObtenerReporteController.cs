using Persistencia;
using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class ApiObtenerReporteController : ApiController
    {

        private Repositorio repositorio;

        public ApiObtenerReporteController()
        {
            this.repositorio = FabricaRepositorio.crearRepositorio();
        }
        /// <summary>
        /// Este metodo permite obtener todos los reportes almacenados en el repositorio en un formato
        /// que pueda ser interpretado, por el cliente que realiza la solicitud del servicio REST
        /// </summary>
        /// <returns>Restona una lista con todos los reportes de tipo Reporte (Models)</returns>
        [HttpGet]
        // GET: api/ApiObtenerReporte
        public IEnumerable<Reporte> consultarReportes()
        {
            return this.convertirREPORTES(this.repositorio.consultarReportes());
        }
        /// <summary>
        /// Este metodo permite convertir una lista de reportes de tipo de dato de base de datos
        /// y los transforma una lista de datos que pueden ser interpretados, es decir, una lista
        /// de Reporte (Models)
        /// </summary>
        /// <param name="reportesdb">Lista de objetos de tipo de datos de base de datos</param>
        /// <returns>Una lista con los reportes en un formato que pueda ser interpretado, es decir,
        /// en un tipo de dato Reporte</returns>
        private IList<Reporte> convertirREPORTES(IList<REPORTE> reportesdb)
        {
            IList<Reporte> reportes = new List<Reporte>();
            foreach (REPORTE reporte in reportesdb)
            {
                reportes.Add(new Reporte() {
                    codigo = reporte.CODREPORTE,
                    rutaGrafico = reporte.RUTAGRAFICO
                });
            }
            return reportes;
        }
        /// <summary>
        /// Este metodo permite buscar un reporte en especifico en el repositorio a partir de su
        /// codigo. Dicho reporte encontrado en el repositorio es transformado en un formato que pueda 
        /// ser interpretado, es decir, se convierte de REPORTE (base de datos) a Reporte (Models)
        /// </summary>
        /// <param name="codReporte"> Codigo del reporte a buscar</param>
        /// <returns>Retorna un reporte si el codigo ingresado existe o null en caso contrario</returns>
        // GET: api/ApiObtenerReporte/5
        [HttpGet]
        public Reporte buscarReporte(string codReporte)
        {
            return this.convertirREPORTE(this.repositorio.buscarReporte(codReporte));
        }
        /// <summary>
        /// Este metodo permite cambiar el tipo de dato en que se va a retornar un objeto. 
        /// En este caso se recibe como parametro un objeto REPORTE (tipo de dato de la base de datos) y es
        /// retornado un objeto de tipo Reporte (Models) que puede ser interpretado por los clientes
        /// que realicen una solicitud REST
        /// </summary>
        /// <param name="reportedb">Reporte de tipo de base de datos</param>
        /// <returns>Retorna un reporte en formato Reporte (Models)</returns>
        private Reporte convertirREPORTE(REPORTE reportedb)
        {
            if (reportedb != null)
            {
                return new Reporte()
                {
                    codigo = reportedb.CODREPORTE,
                    rutaGrafico = reportedb.RUTAGRAFICO
                };
            }
            return null;
        }        
    }
}
