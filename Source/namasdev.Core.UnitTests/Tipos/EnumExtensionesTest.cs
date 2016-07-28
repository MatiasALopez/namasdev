using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using namasdev.Tipos;
using namasdev.Core.UnitTests._TestUtils;

namespace namasdev.Core.UnitTests.Tipos
{
    [TestClass]
    public class EnumExtensionesTest
    {
        #region Descripcion

        [TestMethod]
        public void Descripcion_SinAtributo_DevuelveTextoDeCampo()
        {
            Enumeracion valor = Enumeracion.Opcion4;

            string descripcion = valor.Descripcion();

            Assert.AreEqual(
                expected: "Opcion4",
                actual: descripcion);
        }

        [TestMethod]
        public void Descripcion_CampoInexistente_DevuelveValorIngresado()
        {
            Enumeracion valor = (Enumeracion)99;

            string descripcion = valor.Descripcion();

            Assert.AreEqual(
                expected: "99",
                actual: descripcion);
        }

        [TestMethod]
        public void Descripcion_ValorOk_ResultadoOk()
        {
            Enumeracion valor = Enumeracion.Opcion1;

            string descripcion = valor.Descripcion();

            Assert.AreEqual(
                expected: "Opción 1",
                actual: descripcion);
        }

        #endregion

        #region EsValido

        [TestMethod]
        public void EsValido_CampoInexistente_DevuelveFalso()
        {
            Enumeracion valor = (Enumeracion)99;

            bool esValido = valor.EsValido();

            Assert.AreEqual(
                expected: false,
                actual: esValido);
        }

        [TestMethod]
        public void EsValido_ValorOk_DevuelveTrue()
        {
            Enumeracion valor = Enumeracion.Opcion1;

            bool esValido = valor.EsValido();

            Assert.AreEqual(
                expected: true,
                actual: esValido);
        }

        #endregion
    }
}
