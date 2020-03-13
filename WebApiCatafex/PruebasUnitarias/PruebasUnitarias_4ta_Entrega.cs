using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Controllers;
using WebService.Models;

namespace PruebasUnitarias
{
    [TestClass]
    public class PruebasUnitarias_4ta_Entrega
    {
        private ApiNotificacionController apiNotificacion;
        private ApiObtenerReporteController apiObtenerReporte;

        public PruebasUnitarias_4ta_Entrega()
        {
            this.apiNotificacion = new ApiNotificacionController();
            this.apiObtenerReporte = new ApiObtenerReporteController();
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
            var response = apiNotificacion.enviarNotificacion(correoDestinatario,asunto,mensaje);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Enviar un correo a un catador, donde su correo es falso
        /// </summary>
        [TestMethod]
        public void enviarCorreoSiEsFalso()
        {
            string correoDestinatario = "miCorreo@gmail.com";
            string asunto = "Usted ha sido asignado para catar";
            string mensaje = "Va a catar el café buendía  en el evento  café de caldas";
            var response = apiNotificacion.enviarNotificacion(correoDestinatario, asunto, mensaje);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
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
            var response = apiNotificacion.enviarNotificacion(correoDestinatario, asunto, mensaje);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }
    }
}
