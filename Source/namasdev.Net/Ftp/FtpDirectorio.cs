using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.Net.Ftp
{
    public class FtpDirectorio : IFtpEntrada
    {
        private string _nombre;

        public FtpDirectorio(Uri uri, IFtpEntrada[] entradas)
        {
            Validador.ValidarRequerido(uri, "uri");

            this.Uri = uri;
            this.Entradas = entradas ?? new IFtpEntrada[0];
        }

        public string Nombre
        {
            get { return _nombre ?? (_nombre = FtpEntradaUtilidades.ObtenerNombreDirectorioDesdeUri(this.Uri)); }
        }

        public Uri Uri { get; private set; }
        public IFtpEntrada[] Entradas { get; private set; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
