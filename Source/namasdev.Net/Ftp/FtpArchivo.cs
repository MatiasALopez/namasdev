using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.Net.Ftp
{
    public class FtpArchivo : IFtpEntrada
    {
        private string _nombre;

        public FtpArchivo(Uri uri)
        {
            Validador.ValidarRequerido(uri, "uri");

            this.Uri = uri;
        }

        public string Nombre 
        {
            get { return _nombre ?? (_nombre = FtpEntradaUtilidades.ObtenerNombreArchivoDesdeUri(this.Uri)); } 
        }

        public Uri Uri { get; private set; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
