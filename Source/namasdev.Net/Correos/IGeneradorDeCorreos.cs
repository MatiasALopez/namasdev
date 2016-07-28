using System;
using System.Net.Mail;

namespace namasdev.Net.Correos
{
    public interface IGeneradorDeCorreos
    {
        MailMessage GenerarCorreo<TModelo>(CorreoParametros parametros, TModelo modelo = null)
            where TModelo : class;
    }
}
