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
        [HttpGet]
        // GET: api/ApiObtenerReporte
        public IEnumerable<Reporte> consultarReportes()
        {
            return this.convertirREPORTES(this.repositorio.consultarReportes());
        }

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

        // GET: api/ApiObtenerReporte/5
        [HttpGet]
        public Reporte buscarReporte(string codReporte)
        {
            return this.convertirREPORTE(this.repositorio.buscarReporte(codReporte));
        }

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

        private IList<Panel> panelesTerminados()
        {
            IList<Panel> paneles = new List<Panel>();
            foreach(PANEL paneldb in repositorio.consultarPaneles())
            {
              
            }
            return paneles;
        }
        // POST: api/ApiObtenerReporte
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiObtenerReporte/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiObtenerReporte/5
        public void Delete(int id)
        {
        }




    }
}
