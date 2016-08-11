using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.Net.Ftp
{
    public class FtpCliente
    {
        private const int DESCARGA_ARCHIVO_TAMAÑO_BUFFER = 2048;

        public FtpCliente(Uri uri)
        {
            Validador.ValidarRequerido(uri, "uri");

            this.Uri = uri;
        }

        public Uri Uri { get; private set; }
        public ICredentials Credenciales { get; set; }

        public IFtpEntrada[] ObtenerEntradas(string nombreDirectorio)
        {
            var nombresEntradas = ObtenerNombresEntradas(nombreDirectorio);

            var entradas = new List<IFtpEntrada>();
            string nombreEntradaSinDirectorio  = null;
            Uri entradaUri = null;
            foreach (var nombreEntrada in nombresEntradas)
            {
                nombreEntradaSinDirectorio = FtpEntradaUtilidades.ObtenerNombreEntradaSinDirectorio(nombreEntrada);
                entradaUri = FtpEntradaUtilidades.CrearEntradaUri(this.Uri, nombreDirectorio, nombreEntradaSinDirectorio);

                if (FtpEntradaUtilidades.EsArchivo(nombreEntrada))
                {
                    entradas.Add(new FtpArchivo(entradaUri));
                }
                else
                {
                    entradas.Add(new FtpDirectorio(entradaUri, ObtenerEntradas(FtpEntradaUtilidades.CombinarPartesUri(nombreDirectorio, nombreEntradaSinDirectorio))));
                }
            }

            return entradas.ToArray();
        }

        private string[] ObtenerNombresEntradas(string directorio = null)
        {
            var request = CrearRequestConCredenciales(new Uri(this.Uri, directorio), WebRequestMethods.Ftp.ListDirectory);

            var res = new List<string>();

            using (var response = (FtpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                while (!reader.EndOfStream)
                {
                    res.Add(reader.ReadLine());
                }

                return res.ToArray();
            }
        }

        public void DescargarArchivo(Uri uri, string pathArchivoDestino)
        {
            var request = CrearRequestConCredenciales(uri, WebRequestMethods.Ftp.DownloadFile);

            request.UseBinary = true;
            request.Proxy = null;

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                ValidarResponseSinErrores(response);

                using (var responseStream = response.GetResponseStream())
                using (var fileStream = new FileStream(pathArchivoDestino, FileMode.Create))
                {
                    byte[] buffer = new byte[DESCARGA_ARCHIVO_TAMAÑO_BUFFER];
                    while(true)
                    {
                        int readCount = responseStream.Read(buffer, 0, buffer.Length);
                        if (readCount == 0)
                        {
                            break;
                        }

                        fileStream.Write(buffer, 0, readCount);
                    }
                }
            }
        }

        public void EliminarArchivo(Uri uri)
        {
            var request = CrearRequestConCredenciales(uri, WebRequestMethods.Ftp.DeleteFile);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                ValidarResponseSinErrores(response);
            }
        }

        public void EliminarDirectorio(Uri uri)
        {
            var request = CrearRequestConCredenciales(uri, WebRequestMethods.Ftp.RemoveDirectory);
            
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                ValidarResponseSinErrores(response);
            }
        }

        private FtpWebRequest CrearRequestConCredenciales(Uri uri, string metodo)
        {
            var request = (FtpWebRequest)FtpWebRequest.CreateDefault(uri);
            request.Method = metodo;

            if (this.Credenciales != null)
            {
                request.Credentials = this.Credenciales;
            }

            return request;
        }

        private void ValidarResponseSinErrores(FtpWebResponse response)
        {
            if (response.StatusCode != FtpStatusCode.FileActionOK)
            {
                throw new FtpException(response.StatusCode, response.StatusDescription);
            }
        }

        public override string ToString()
        {
            return this.Uri.AbsoluteUri;
        }
    }
}
