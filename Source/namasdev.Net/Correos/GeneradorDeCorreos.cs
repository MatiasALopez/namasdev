using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using namasdev.Templates;
using namasdev.Tipos;
using namasdev.Validaciones;

namespace namasdev.Net.Correos
{
    public class GeneradorDeCorreos : IGeneradorDeCorreos
    {
        private IRepositorioPlantillas _repositorioPlantillas;
        private IGeneradorDeContenido _generadorDeContenido;

        public GeneradorDeCorreos(IRepositorioPlantillas repositorioPlantillas, IGeneradorDeContenido generadorDeContenido)
        {
            Validador.ValidarRequerido(repositorioPlantillas, "repositorioPlantillas");
            Validador.ValidarRequerido(generadorDeContenido, "generadorDeContenido");

            _repositorioPlantillas = repositorioPlantillas;
            _generadorDeContenido = generadorDeContenido;

            Encoding = Encoding.UTF8;
            Headers = new NameValueCollection();
        }

        /// <summary>
        /// Encoding que se aplicará a: Asunto, Cuerpo y Headers. Por default, UTF-8.
        /// </summary>
        public Encoding Encoding { get; set; }
        public NameValueCollection Headers { get; set; }

        public MailMessage GenerarCorreo(CorreoParametros parametros)
        {
            return GenerarCorreo<object>(parametros, null);
        }

        public MailMessage GenerarCorreo<TModelo>(CorreoParametros parametros, TModelo modelo)
            where TModelo : class
        {
            var correo = new MailMessage();

            if (Encoding != null)
            {
                correo.SubjectEncoding = Encoding;
                correo.BodyEncoding = Encoding;
                correo.HeadersEncoding = Encoding;
            }

            if (Headers != null)
            {
                correo.Headers.AgregarOReemplazar(Headers);
            }
                
            if (parametros != null)
            {
                correo.Headers.AgregarOReemplazar(parametros.Headers);

                if (parametros.Remitente != null)
                {
                    correo.From = parametros.Remitente;
                }

                if (!String.IsNullOrWhiteSpace(parametros.Asunto))
                {
                    correo.Subject = parametros.Asunto;
                }

                if (!String.IsNullOrWhiteSpace(parametros.NombrePlantilla))
                {
                    correo.Body = GenerarContenidoDeCorreo(parametros.NombrePlantilla, modelo);
                }

                correo.IsBodyHtml = parametros.EsHtml;
            }

            return correo;
        }

        private string GenerarContenidoDeCorreo<TModelo>(string nombrePlantilla, 
            TModelo modelo = null)
            where TModelo : class
        {
            var plantilla = _repositorioPlantillas.Obtener(nombrePlantilla);
            return _generadorDeContenido.GenerarContenido(plantilla, modelo);
        }
    }
}
