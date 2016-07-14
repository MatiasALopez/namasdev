using namasdev.Templates;
using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Confidenza.Infraestructura.Correos
{
    public class ServicioGeneradorDeCorreos : IServicioGeneradorDeCorreos
    {
        private readonly Encoding ENCODING = Encoding.UTF8;

        private IRepositorioPlantillas _repositorioPlantillas;
        private IGeneradorDeContenido _generadorDeContenido;

        public ServicioGeneradorDeCorreos(IRepositorioPlantillas repositorioPlantillas, IGeneradorDeContenido generadorDeContenido)
        {
            Validador.ValidarRequerido(repositorioPlantillas, "repositorioPlantillas");
            Validador.ValidarRequerido(generadorDeContenido, "generadorDeContenido");

            _repositorioPlantillas = repositorioPlantillas;
            _generadorDeContenido = generadorDeContenido;
        }

        public MailMessage GenerarCorreo<TModelo>(CorreoParametros parametros, 
            TModelo modelo = null)
            where TModelo : class
        {
            var correo = new MailMessage();

            correo.From = parametros.Remitente;

            correo.Subject = parametros.Asunto;
            correo.SubjectEncoding = ENCODING;

            correo.Body = GenerarContenidoDeCorreo(parametros.NombrePlantilla, modelo);
            correo.BodyEncoding = ENCODING;

            correo.IsBodyHtml = parametros.EsHtml;

            return correo;
        }

        public string GenerarContenidoDeCorreo<TModelo>(string nombrePlantilla, 
            TModelo modelo = null)
            where TModelo : class
        {
            var plantilla = _repositorioPlantillas.Obtener(nombrePlantilla);
            return _generadorDeContenido.GenerarContenido(plantilla, modelo);
        }
    }
}
