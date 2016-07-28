using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using namasdev.Core.UnitTests._TestUtils;
using namasdev.Validaciones;

namespace namasdev.Core.UnitTests.Validaciones
{
    [TestClass]
    public class EnumValidoAttributeTest
    {
        [TestMethod]
        public void EnumConValor_PasaValidacion()
        {
            var obj = new ObjetoConEnum { Enumeracion = Enumeracion.Opcion1 };

            var resultado = Validador.ObtenerErroresDeValidacion(obj);

            Assert.IsFalse(resultado.Any());
        }

        [TestMethod]
        public void EnumSinValor_NoPasaValidacion()
        {
            var obj = new ObjetoConEnum();

            var resultado = Validador.ObtenerErroresDeValidacion(obj);

            Assert.AreEqual(
                expected: 1,
                actual: resultado.Count());
        }
    }
}
