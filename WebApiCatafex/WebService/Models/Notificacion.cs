using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public interface Notificacion
    {
         bool enviarMensaje(string destinatario, string asunto, string mensaje);
    }
}