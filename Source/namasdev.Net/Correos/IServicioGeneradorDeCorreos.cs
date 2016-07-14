using System;
using System.Net.Mail;

namespace Confidenza.Infraestructura.Correos
{
    public interface IServicioGeneradorDeCorreos
    {
        string GenerarContenidoDeCorreo<TModelo>(string nombrePlantilla, TModelo modelo = null)
            where TModelo : class;
        MailMessage GenerarCorreo<TModelo>(CorreoParametros parametros, TModelo modelo = null)
            where TModelo : class;
    }
}
