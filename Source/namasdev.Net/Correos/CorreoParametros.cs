using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Net.Correos
{
    public class CorreoParametros
    {
        public CorreoParametros(string asunto, MailAddress remitente, string nombrePlantilla, bool esHtml, NameValueCollection headers)
        {
            Asunto = asunto;
            Remitente = remitente;
            NombrePlantilla = nombrePlantilla;
            EsHtml = esHtml;
            Headers = headers;
        }

        public string Asunto { get; set; }
        public MailAddress Remitente { get; set; }
        public string NombrePlantilla { get; set; }
        public bool EsHtml { get; set; }
        public NameValueCollection Headers { get; set; }
    }
}
