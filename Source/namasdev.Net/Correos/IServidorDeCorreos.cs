using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Confidenza.Infraestructura.Correos
{
    public interface IServidorDeCorreos
    {
        void EnviarCorreo(MailMessage correo);
    }
}
