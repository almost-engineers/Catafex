using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Controllers;
using WebService.Models;
using Persistencia;
using Moq;

namespace PruebasUnitarias
{
    [TestClass]
    public class PruebasUnitarias_4Ta_Entrega
    {
        readonly private Mock<IRepositorio> mockRepositorio;
        readonly private ApiNotificacionController apiNotificacion;
        readonly private ApiObtenerReporteController apiObtenerReporte;
        const string arregloBytes="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";
        readonly System.Text.UTF8Encoding encoding;

        public PruebasUnitarias_4Ta_Entrega()
        {
            this.apiNotificacion = new ApiNotificacionController();
            this.mockRepositorio = new Mock<IRepositorio>();
            this.apiObtenerReporte = new ApiObtenerReporteController(mockRepositorio.Object);
            encoding = new System.Text.UTF8Encoding();
        }

        /// <summary>
        /// metodo de prueba para el verificar el envío del correo a un catador 
        /// cuyos valores son validos
        /// </summary>
        [TestMethod]
        public void enviarCorreoExitosamente()
        {
            string correoDestinatario = "julisalgado71@gmail.com";
            string asunto = "Usted ha sido asignado para catar";
            string mensaje = "Va a catar el café buendía  en el evento  café de caldas";
            var response = this.apiNotificacion.enviarNotificacion(correoDestinatario,asunto,mensaje);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Enviar un correo a un catador, donde su correo es Invalido
        /// </summary>
        [TestMethod]
        public void enviarCorreoSiEsInvalido()
        {
            string correoDestinatario = "esteEsmiCorreo";
            string asunto = "Usted ha sido asignado para catar";
            string mensaje = "Va a catar el café buendía  en el evento  café de caldas";
            var response = this.apiNotificacion.enviarNotificacion(correoDestinatario, asunto, mensaje);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.PreconditionFailed);
        }

        /// <summary>
        /// Intentar enviar un correo a un catador, donde el correo es vacio    
        /// </summary>
        [TestMethod]
        public void enviarCorreoSiEsVacio()
        {
            string correoDestinatario = "";
            string asunto = "Usted ha sido asignado para catar";
            string mensaje = "Va a catar el café buendía  en el evento  café de caldas";
            var response = this.apiNotificacion.enviarNotificacion(correoDestinatario, asunto, mensaje);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.PreconditionFailed);
        }


        /// <summary>
        /// Obtener el gráfico de un panel el cual ya haya terminado  
        /// </summary>
        [TestMethod]
        public void ObtenerGraficoPanelTerminado()
        {
            string codPanel = "CP-4";
            this.mockRepositorio.Setup(m => m.ExistePanel(codPanel)).Returns(false);
            this.mockRepositorio.Setup(m => m.PanelTerminado(codPanel)).Returns(true);
            this.mockRepositorio.Setup(m => m.GenerarImagen(codPanel)).Returns(encoding.GetBytes(arregloBytes));
            byte [] arregloBytesReal = this.apiObtenerReporte.obtenerGrafico(codPanel);
            Assert.AreEqual(encoding.GetString(arregloBytesReal), arregloBytes);
        }

        /// <summary>
        /// Obtener el gráfico de un panel donde no se hayan terminados todas sus catas
        /// </summary>
        [TestMethod]
        public void ObtenerGraficoPanelNoTerminado()
        {
            string codPanel = "CP-1";
            mockRepositorio.Setup(m => m.ExistePanel(codPanel)).Returns(true);
            mockRepositorio.Setup(m => m.PanelTerminado(codPanel)).Returns(false);
            byte[] arregloBytesReal = this.apiObtenerReporte.obtenerGrafico(codPanel);
            Assert.AreEqual(null, arregloBytesReal);
        }

        /// <summary>
        /// Intentar Obtener el gráfico de un panel donde su código no sea correcto
        [TestMethod]
        public void ObtenerGraficoCódigoPanelInvalido()
        {
            string codPanel = "codPanel";
            mockRepositorio.Setup(m => m.ExistePanel(codPanel)).Returns(true);
            byte[] arregloBytesReal = this.apiObtenerReporte.obtenerGrafico(codPanel);
            Assert.AreEqual(null, arregloBytesReal);
        }

        /// <summary>
        /// Intentar Obtener el gráfico en cuál el código del panel este vacío
        /// </summary>
        [TestMethod]
        public void ObtenerGraficoCódigoPanelVacio()
        {
            string codPanel = "";
            byte[] arregloBytesReal = this.apiObtenerReporte.obtenerGrafico(codPanel);
            Assert.AreEqual(null, arregloBytesReal);
        }

        /// <summary>
        /// Obtener las observaciones de un panel terminado 
        /// </summary>
        [TestMethod]
        public void ObtenerObservacionesPanelTerminado()
        {
            string codPanel = "CP-4";
            this.mockRepositorio.Setup(m => m.PanelTerminado(codPanel)).Returns(true);
            var response = this.apiObtenerReporte.obtenerObservaciones(codPanel);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Obtener las observaciones de un panel donde no se hayan terminados todas sus catas
        /// </summary>
        [TestMethod]
        public void ObtenerObservacionesPanelNoTerminado()
        {
            string codPanel = "CP-1";
            this.mockRepositorio.Setup(m => m.PanelTerminado(codPanel)).Returns(false);
            var response = this.apiObtenerReporte.obtenerObservaciones(codPanel);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }


        /// <summary>
        /// Intentar Obtener las observaciones de un panel donde su código no sea correcto 
        /// </summary>
        [TestMethod]
        public void ObtenerObservacionesCódigoPanelInvalido()
        {
            string codPanel = "codPanel";
            this.mockRepositorio.Setup(m => m.ExistePanel(codPanel)).Returns(true);
            var response = this.apiObtenerReporte.obtenerObservaciones(codPanel);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Intentar Obtener el gráfico en cuál el código del panel este vacío
        /// </summary>
        [TestMethod]
        public void ObtenerObservacionesCódigoPanelVacio()
        {
            string codPanel = "";
            var response = this.apiObtenerReporte.obtenerObservaciones(codPanel);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
    }
}
