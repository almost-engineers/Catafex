using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class ApiNotificacionController : ApiController
    {

        private Correo correo = new Correo();
        [HttpPost]
        [Route("api/Notificacion/enviarMensaje")]
        public HttpResponseMessage enviarNotificacion(string correoDestinatario, string asunto, string mensaje)
        {
            HttpResponseMessage response;
            if (correo.enviarMensaje(correoDestinatario, asunto, mensaje))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Mensaje enviado exitosamente");
                return response;

            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("No se ha podido establecer conexion");
                return response;
            }
        }
    }
}
