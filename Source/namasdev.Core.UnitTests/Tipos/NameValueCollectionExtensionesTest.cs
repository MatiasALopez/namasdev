using System;
using System.Collections.Specialized;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using namasdev.Tipos;

namespace namasdev.Core.UnitTests.Tipos
{
    [TestClass]
    public class NameValueCollectionExtensionesTest
    {
        #region AgregarOReemplazar

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void AgregarOReemplazar_ColeccionNull_ArrojaExcepcion()
        {
            NameValueCollectionExtensiones.AgregarOReemplazar(null, null);
        }

        [TestMethod]
        public void AgregarOReemplazar_ColeccionVaciaYNuevosValoresNull_ColeccionSinModificaciones()
        {
            NameValueCollection coleccion = new NameValueCollection();
            
            coleccion.AgregarOReemplazar(null);

            Assert.IsTrue(coleccion.Count == 0);
        }

        [TestMethod]
        public void AgregarOReemplazar_ColeccionVaciaYNuevosValoresVacia_ColeccionSinModificaciones()
        {
            string nombre = "nombre1",
                valor = "valor1";

            NameValueCollection coleccion = new NameValueCollection();
            coleccion.Add(nombre, valor);

            NameValueCollection nuevosValores = new NameValueCollection();

            coleccion.AgregarOReemplazar(nuevosValores);

            CollectionAssert.AreEqual(
                expected: new NameValueCollection(coleccion),
                actual: coleccion);
        }

        [TestMethod]
        public void AgregarOReemplazar_ColeccionConElementosYNuevosValores_SeAgregarYReemplazanValoresOk()
        {
            string nombre1 = "nombre1",
                nombre2 = "nombre2",
                nombre3 = "nombre3",
                valor1 = "valor1",
                valor2Original = "valor2",
                valor2Nuevo = "valor2*",
                valor3 = "valor3";

            NameValueCollection coleccion = new NameValueCollection();
            coleccion.Add(nombre1, valor1);
            coleccion.Add(nombre2, valor2Original);

            NameValueCollection nuevosValores = new NameValueCollection();
            nuevosValores.Add(nombre2, valor2Nuevo);
            nuevosValores.Add(nombre3, valor3);

            coleccion.AgregarOReemplazar(nuevosValores);

            var esperada = new NameValueCollection();
            esperada.Add(nombre1, valor1);
            esperada.Add(nombre2, valor2Nuevo);
            esperada.Add(nombre3, valor3);

            CollectionAssert.AreEqual(
                expected: esperada,
                actual: coleccion);
        }

        #endregion
    }
}
