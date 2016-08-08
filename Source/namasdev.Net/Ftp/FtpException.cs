using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.Net.Ftp
{
    [Serializable]
    public class FtpException : Exception
    {
        public FtpException() { }
        public FtpException(string message) : base(message) { }
        public FtpException(string message, Exception inner) : base(message, inner) { }

        public FtpException(FtpStatusCode statusCode, string statusCodeDescripcion)
            : this(statusCodeDescripcion, statusCode, statusCodeDescripcion, null)
        {
        }

        public FtpException(string message, FtpStatusCode statusCode, string statusCodeDescripcion)
            :this(message, statusCode, statusCodeDescripcion, null)
        {
        }

        public FtpException(string message, FtpStatusCode statusCode, string statusCodeDescripcion, Exception inner)
            : this(message, inner)
        {
            Validador.ValidarRequerido(statusCodeDescripcion, "statusCodeDescripcion");

            this.StatusCode = StatusCode;
            this.StatusCodeDescripcion = statusCodeDescripcion;
        }

        protected FtpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public FtpStatusCode StatusCode { get; private set; }
        public string StatusCodeDescripcion { get; private set; }
    }
}
