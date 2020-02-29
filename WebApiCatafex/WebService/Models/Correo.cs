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

        private void construirMensaje(string correoDestino, string asunto, string mensaje)
        {
            this.mailMessage = new MailMessage(this.correo_origen, correoDestino, asunto, mensaje);
            this.mailMessage.IsBodyHtml = false;
        }

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