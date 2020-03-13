using Persistencia;
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
        /// <summary>
        /// Este metodo se encarga de enviar una notificacion de manera generica, lo hace a partir de una clase que implementa
        /// una interfaz con un metodo generico de envio de informacion. A pesar de estar dentro de un api, solo es para uso interno
        /// de las demas apis, no es un servicio que pueda ser utilizado por un cliente, sin embargo, puede ser utilizado si se habilita el 
        /// metodo.
        /// </summary>
        /// <param name="correoDestinatario">el correo de la persona a la cual se le quiere enviar el correo o mensaje</param>
        /// <param name="asunto">El asunto que debe contener el mensaje, puede ser un preambulo del contenido de este</param>
        /// <param name="mensaje">Toda la informacion que se quiere enviar al destinatario</param>
        /// <returns>un mensaje de exito o error, junto con su correspondiente codigo HttpSatusCode, OK y BadRequest
        /// respectivamente</returns>
        public HttpResponseMessage enviarNotificacion(string correoDestinatario, string asunto, string mensaje)
        {
            HttpResponseMessage response;
            if (correo.correoValido(correoDestinatario))
            {
                if (correo.enviarMensaje(correoDestinatario, asunto, mensaje))
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent("Mensaje enviado exitosamente");
                    return response;

                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent("No se ha podido establecer conexion");
                    return response;
                }
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed);
                response.Content = new StringContent("El correo no es valido");
                return response;
            }
            
        }
    }
}
