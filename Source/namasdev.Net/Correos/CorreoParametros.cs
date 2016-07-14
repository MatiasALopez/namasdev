using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Confidenza.Infraestructura.Correos
{
    public class CorreoParametros
    {
        public CorreoParametros(string asunto, MailAddress remitente, string nombrePlantilla, bool esHtml)
        {
            Asunto = asunto;
            Remitente = remitente;
            NombrePlantilla = nombrePlantilla;
            EsHtml = esHtml;
        }

        public string Asunto { get; set; }
        public MailAddress Remitente { get; set; }
        public string NombrePlantilla { get; set; }
        public bool EsHtml { get; set; }
    }
}
