using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.IO
{
    public static class StreamExtensiones
    {
        public static byte[] LeerTodosBytes(this Stream stream)
        {
            Validador.ValidarRequerido(stream, "stream");
            
            int length = (int)stream.Length;
            var bytes = new byte[length];
            stream.Read(bytes, 0, length);

            return bytes;
        }
    }
}
