using System;
using System.Net.Mail;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using namasdev.Net.Correos;
using System.Collections.Specialized;

namespace namasdev.Net.UnitTests.Correos
{
    [TestClass]
    public class CorreoParametrosTest
    {
        #region Constructor

        [TestMethod]
        public void Ctor_ValoresDefault_ResultadoOk()
        {
            string asunto = null,
                nombrePlantilla = null;
            MailAddress remitente = null;
            bool esHtml = false;
            NameValueCollection headers = null;

            var parametros = new CorreoParametros(asunto, remitente, nombrePlantilla, esHtml, headers);

            AssertCorreoParametros(parametros,
                asunto, remitente, nombrePlantilla, esHtml, headers);
        }

        [TestMethod]
        public void Ctor_Valores_ResultadoOk()
        {
            string asunto = "Asunto",
                nombrePlantilla = "Plantilla";
            MailAddress remitente = new MailAddress("remitente@mail.com", "Remitente");
            bool esHtml = true;
            NameValueCollection headers = new NameValueCollection();
            headers.Add("header1", "value1");

            var parametros = new CorreoParametros(asunto, remitente, nombrePlantilla, esHtml, headers);

            AssertCorreoParametros(parametros,
                asunto, remitente, nombrePlantilla, esHtml, headers);
        }

        #endregion

        private void AssertCorreoParametros(CorreoParametros obj, 
            string asunto, MailAddress remitente, string nombrePlantilla, bool esHtml, NameValueCollection headers)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            Assert.AreEqual(
                expected: asunto,
                actual: obj.Asunto,
                message: "Asunto");

            Assert.AreEqual(
                expected: remitente,
                actual: obj.Remitente,
                message: "Remitente");

            Assert.AreEqual(
                expected: nombrePlantilla,
                actual: obj.NombrePlantilla,
                message: "NombrePlantilla");

            Assert.AreEqual(
                expected: esHtml,
                actual: obj.EsHtml,
                message: "EsHtml");

            CollectionAssert.AreEqual(
               expected: headers,
               actual: obj.Headers,
               message: "Headers");
        }
    }
}
