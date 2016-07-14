using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnamSPPE.Infraestructura.IO
{
    public static class StreamExtensiones
    {
        public static byte[] LeerTodosBytes(this Stream stream)
        {
            long length = stream.Length;
            var bytes = new byte[length];
            stream.Read(bytes, 0, (int)length);

            return bytes;
        }
    }
}
