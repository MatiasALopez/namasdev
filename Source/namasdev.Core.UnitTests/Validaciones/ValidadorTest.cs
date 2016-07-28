using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using namasdev.Validaciones;
using namasdev.Core.UnitTests._TestUtils;
using namasdev.Excepciones;

namespace namasdev.Core.UnitTests.Validaciones
{
    [TestClass]
    public class ValidadorTest
    {
        #region ValidarRequerido

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ValidarRequerido_ValorNull_ArrojaExcepcion()
        {
            Validador.ValidarRequerido(null, "nombre");
        }

        [TestMethod,
        ExpectedException(typeof(ApplicationException))]
        public void ValidarRequerido_ValorNullYExcepcionEspecificada_ArrojaExcepcion()
        {
            Validador.ValidarRequerido<ApplicationException>(null, "nombre");
        }

        [TestMethod]
        public void ValidarRequerido_ValorNull_ExcepcionConDatosCorrectos()
        {
            string nombre = "nombre";
            try
            {
                Validador.ValidarRequerido(null, nombre);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(
                    expected: nombre,
                    actual: ex.ParamName);
            }
        }

        [TestMethod]
        public void ValidarRequerido_ValorNullYExcepcionEspecificada_ExcepcionConDatosCorrectos()
        {
            string nombre = "nombre";
            try
            {
                Validador.ValidarRequerido<ApplicationException>(null, nombre);
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(
                    expected: nombre,
                    actual: ex.Message);
            }
        }

        #endregion

        #region ObtenerErroresDeValidacion

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerErroresDeValidacion_ObjetoNull_ArrojaExcepcion()
        {
            Objeto obj = null;
            Validador.ObtenerErroresDeValidacion(obj);
        }

        [TestMethod]
        public void ObtenerErroresDeValidacion_ObjetoConErrores_DevuelveListaDeErrores()
        {
            var obj = CrearObjeto(tieneErrores: true);

            var resultado = Validador.ObtenerErroresDeValidacion(obj);

            Assert.AreEqual(
                expected: 2,
                actual: resultado.Count(),
                message: "Propiedades");

            obj.Entero = 1;
            obj.FechaHora = DateTime.Now;

            resultado = Validador.ObtenerErroresDeValidacion(obj);

            Assert.AreEqual(
                expected: 1,
                actual: resultado.Count(),
                message: "Metodo Validate");
        }

        [TestMethod]
        public void ObtenerErroresDeValidacion_ObjetoSinErrores_DevuelveListaVacia()
        {
            var obj = CrearObjeto(tieneErrores: false);

            var resultado = Validador.ObtenerErroresDeValidacion(obj);

            Assert.IsFalse(resultado.Any());
        }

        #endregion

        #region Validar

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void Validar_ObjetoNull_ArrojaNullExcepcion()
        {
            Objeto obj = null;
            Validador.Validar(obj);
        }

        [TestMethod,
        ExpectedException(typeof(ExcepcionMensajeAlUsuario))]
        public void Validar_ObjetoConErrores_ArrojaExcepcion()
        {
            Objeto obj = CrearObjeto(tieneErrores: true);
            Validador.Validar(obj);
        }

        [TestMethod]
        public void Validar_ObjetoSinErrores_NoArrojaExcepcion()
        {
            Objeto obj = CrearObjeto(tieneErrores: false);
            Validador.Validar(obj);
        }

        [TestMethod]
        public void Validar_ObjetoConErroresYEncabezado_GeneraExcepcionConEncabezado()
        {
            string mensajeEncabezado = "Encabezado.";

            Objeto obj = CrearObjeto(tieneErrores: true);
            
            try
            {
                Validador.Validar(obj,
                    mensajeEncabezado: mensajeEncabezado);
            }
            catch (ExcepcionMensajeAlUsuario ex)
            {
                Assert.IsTrue(ex.Message.StartsWith(mensajeEncabezado));
            }
        }

        #endregion

        private Objeto CrearObjeto(bool tieneErrores)
        {
            var obj = new Objeto();
            if (!tieneErrores)
            {
                obj.Texto = "texto";
                obj.Entero = 1;
                obj.FechaHora = DateTime.Now;
            }
            return obj;
        }
    }
}
