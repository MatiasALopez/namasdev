using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using namasdev.IO;
using System.IO;

namespace namasdev.Core.UnitTests.IO
{
    [TestClass]
    public class StreamExtensionesTest
    {
        #region LeerTodosBytes

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void LeerTodosBytes_StreamNull_ArrojaExcepcion()
        {
            Stream stream = null;
            StreamExtensiones.LeerTodosBytes(stream);
        }

        [TestMethod]
        public void LeerTodosBytes_StreamVacio_DevuelveArrayVacio()
        {
            using (var stream = new MemoryStream())
            {
                var resultado = stream.LeerTodosBytes();

                CollectionAssert.AreEqual(
                    expected: new byte[0],
                    actual: resultado);
            }
        }

        [TestMethod]
        public void LeerTodosBytes_StreamConContenido_DevuelveArrayConContenido()
        {
            var contenido = new byte[] { 0, 250, 150, 63, 59 };
            using (var stream = new MemoryStream(contenido))
            {
                var resultado = stream.LeerTodosBytes();

                CollectionAssert.AreEqual(
                    expected: contenido,
                    actual: resultado);
            }
        }

        #endregion
    }
}
