using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebService.Models
{
    public class Correo : Notificacion
    {

        private string correo_origen;
        private string contrasena;
        private MailMessage mailMessage;
        private SmtpClient smtpClient;

        public Correo()
        {
            correo_origen = "informacion.catafex@gmail.com";
            contrasena = "@Catafex";
        }
        /// <summary>
        /// Este metodo se encarga de construir una estructura que permita enviar el correo de manera que no se 
        /// desborde el codigo por una excepcion no esperada.
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public bool enviarMensaje(string destinatario, string asunto, string mensaje)
        {
            this.construirMensaje(destinatario, asunto, mensaje);
            try
            {
                this.enviarMensaje();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Este metodo se encarga de construir el correo apartir de unos elementos basicos, entre los cuales se encuentran,
        /// el correo de la persona que va a recibir el mensaje, el asunto y el contenido de este, ademas de aclarar que el 
        /// mensaje contenido en este, es texto plano y no un html.
        /// </summary>
        /// <param name="correoDestino"></param>
        /// <param name="asunto"></param>
        /// <param name="mensaje"></param>
        private void construirMensaje(string correoDestino, string asunto, string mensaje)
        {
            this.mailMessage = new MailMessage(this.correo_origen, correoDestino, asunto, mensaje);
            this.mailMessage.IsBodyHtml = false;
        }
        /// <summary>
        /// Metodo que se encarga de realizar la conexion con el smtp de gmail, que para este caso es por el cual
        /// se esta realizando el envio de correos, por medio del protocolo udp
        /// </summary>
        private void enviarMensaje()
        {
            this.smtpClient = new SmtpClient("smtp.gmail.com");
            this.smtpClient.EnableSsl = true;
            this.smtpClient.UseDefaultCredentials = false;
            this.smtpClient.Port = 587;
            this.smtpClient.Credentials = new System.Net.NetworkCredential(this.correo_origen, contrasena);
            this.smtpClient.Send(this.mailMessage);
            this.smtpClient.Dispose();
        }
    }
}