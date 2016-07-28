using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using namasdev.Net.Correos;
using namasdev.Net.UnitTests._TestUtils;
using namasdev.Templates;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Text;

namespace namasdev.Net.UnitTests.Correos
{
    [TestClass]
    public class GeneradorDeCorreosTest
    {
        private readonly Plantilla Plantilla1 = new Plantilla("Plantilla1", "Contenido 1");
        private readonly Plantilla Plantilla2 = new Plantilla("Plantilla2", "Contenido 2");
        
        #region Constructor

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_RepositorioPlantillasNull_ArrojaExcepcion()
        {
            new GeneradorDeCorreos(null, null);
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_GeneradorDeContenidoNull_ArrojaExcepcion()
        {
            new GeneradorDeCorreos(null, null);
        }

        #endregion
        
        #region GenerarCorreo

        [TestMethod]
        public void GenerarCorreo_ParametrosNull_GeneraCorreoConEncodingYHeadersDefault()
        {
            var generador = CrearGeneradorDeCorreos();

            using (var resultado = generador.GenerarCorreo(null))
            {
                AssertCorreo(resultado,
                    String.Empty, null, String.Empty, false, generador.Headers, generador.Encoding);
            }
        }

        // TODO (ML): terminar

        #endregion

        private GeneradorDeCorreos CrearGeneradorDeCorreos()
        {
            return new GeneradorDeCorreos(
                new RepositorioPlantillasEnMemoria(new Plantilla[] { Plantilla1, Plantilla2 }),
                new GeneradorDeContenidoSinLogica()
                );
        }

        private void AssertCorreo(MailMessage obj,
            string asunto, MailAddress remitente, string contenido, bool esHtml, NameValueCollection headers, Encoding encoding)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            Assert.AreEqual(
                expected: asunto,
                actual: obj.Subject,
                message: "Subject");

            Assert.AreEqual(
                expected: remitente,
                actual: obj.From,
                message: "From");

            Assert.AreEqual(
                expected: contenido,
                actual: obj.Body,
                message: "Body");

            Assert.AreEqual(
                expected: esHtml,
                actual: obj.IsBodyHtml,
                message: "IsBodyHtml");

            CollectionAssert.AreEqual(
               expected: headers,
               actual: obj.Headers,
               message: "Headers");

            Assert.AreEqual(
                expected: encoding,
                actual: obj.HeadersEncoding,
                message: "HeadersEncoding");

            Assert.AreEqual(
                expected: encoding,
                actual: obj.SubjectEncoding,
                message: "SubjectEncoding");

            Assert.AreEqual(
                expected: encoding,
                actual: obj.BodyEncoding,
                message: "BodyEncoding");
        }
    }
}
