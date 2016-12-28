using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using namasdev.Tipos;
using System.Collections.Generic;

namespace namasdev.Core.UnitTests.Tipos
{
    [TestClass]
    public class StringExtensionesTest
    {
        private const string CADENA_CON_ACENTOS_Y_ESPECIALES = "aáä eéë iíï oóö uúü ñ AÁÄ EÉË IÍÏ OÓÖ UÚÜ Ñ";
        private const string CADENA_SIN_ACENTOS_Y_ESPECIALES = "aaa eee iii ooo uuu n AAA EEE III OOO UUU N";

        private const string CADENA_CON_CARACTERES_NO_ALFANUMERICOS = "0aA¡!¿?+_-|.,:bB1";
        private const string CADENA_SIN_CARACTERES_NO_ALFANUMERICOS = "0aAbB1";

        private const string CADENA = "abcde12345";
        private const string SUFIJO_TRUNCADO = "...";

        private const string SEPARADOR_CLAVE_VALOR = ": ";
        private const string SEPARADOR_ITEM = ", ";

        private const string CADENA_SIN_CAPITALIZACION = "aAbB Cc. d'D";
        private const string CADENA_CON_CAPITALIZACION = "Aabb Cc. D'd";

        #region ReemplazarAcentosYCaracteresEspecialesPorEquivalentes

        [TestMethod]
        public void ReemplazarAcentosYCaracteresEspecialesPorEquivalentes_ValorNullOCadenaVaciaOEspacios_DevuelveCadenaVacia()
        {
            string stringNull = null;
            string resNull = stringNull.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes();

            string resCadenaVacia = String.Empty.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes();
            string resEspacios = "    ".ReemplazarAcentosYCaracteresEspecialesPorEquivalentes();

            Assert.AreEqual(
                expected: String.Empty,
                actual: resNull,
                message: "null");

            Assert.AreEqual(
                expected: String.Empty,
                actual: resCadenaVacia,
                message: "String.Empty");

            Assert.AreEqual(
                expected: String.Empty,
                actual: resEspacios,
                message: "Espacios");
        }

        [TestMethod]
        public void ReemplazarAcentosYCaracteresEspecialesPorEquivalentes_ValorConAcentosYCaracteresEspeciales_ResultadoOk()
        {
            string res = CADENA_CON_ACENTOS_Y_ESPECIALES.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes();

            Assert.AreEqual(
                expected: CADENA_SIN_ACENTOS_Y_ESPECIALES,
                actual: res);
        }

        [TestMethod]
        public void ReemplazarAcentosYCaracteresEspecialesPorEquivalentes_ValorSinAcentosYCaracteresEspeciales_ResultadoOk()
        {
            string res = CADENA_SIN_ACENTOS_Y_ESPECIALES.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes();

            Assert.AreEqual(
                expected: CADENA_SIN_ACENTOS_Y_ESPECIALES,
                actual: res);
        }

        #endregion

        #region QuitarCaracteresNoAlfanumericos

        [TestMethod]
        public void QuitarCaracteresNoAlfanumericos_ValorNullOCadenaVaciaOEspacios_DevuelveCadenaVacia()
        {
            string stringNull = null;
            string resNull = stringNull.QuitarCaracteresNoAlfanumericos();

            string resCadenaVacia = String.Empty.QuitarCaracteresNoAlfanumericos();
            string resEspacios = "    ".QuitarCaracteresNoAlfanumericos();

            Assert.AreEqual(
                expected: String.Empty,
                actual: resNull,
                message: "null");

            Assert.AreEqual(
                expected: String.Empty,
                actual: resCadenaVacia,
                message: "String.Empty");

            Assert.AreEqual(
                expected: String.Empty,
                actual: resEspacios,
                message: "Espacios");
        }

        [TestMethod]
        public void QuitarCaracteresNoAlfanumericos_ValorConCaracteresNoAlfanumericos_ResultadoOk()
        {
            string res = CADENA_CON_CARACTERES_NO_ALFANUMERICOS.QuitarCaracteresNoAlfanumericos();

            Assert.AreEqual(
                expected: CADENA_SIN_CARACTERES_NO_ALFANUMERICOS,
                actual: res);
        }

        [TestMethod]
        public void QuitarCaracteresNoAlfanumericos_ValorSinCaracteresNoAlfanumericos_ResultadoOk()
        {
            string res = CADENA_SIN_CARACTERES_NO_ALFANUMERICOS.QuitarCaracteresNoAlfanumericos();

            Assert.AreEqual(
                expected: CADENA_SIN_CARACTERES_NO_ALFANUMERICOS,
                actual: res);
        }

        #endregion

        #region Truncar

        [TestMethod]
        public void Truncar_ValorNullOCadenaVaciaOEspacios_DevuelveCadenaVacia()
        {
            ushort tamaño = 10;

            string stringNull = null;
            string resNull = stringNull.Truncar(tamaño);
            string resCadenaVacia = String.Empty.Truncar(tamaño);
            string resEspacios = "    ".Truncar(tamaño);

            Assert.AreEqual(
                expected: String.Empty,
                actual: resNull,
                message: "null");

            Assert.AreEqual(
                expected: String.Empty,
                actual: resCadenaVacia,
                message: "String.Empty");

            Assert.AreEqual(
                expected: String.Empty,
                actual: resEspacios,
                message: "Espacios");
        }

        [TestMethod]
        public void Truncar_TamañoMayorALargoDeCadena_ResultadoOk()
        {
            ushort tamaño = (ushort)(CADENA.Length + 5);

            string res = CADENA.Truncar(tamaño);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoMenorALargoDeCadena_ResultadoOk()
        {
            ushort tamaño = (ushort)(CADENA.Length);

            string res = (CADENA + CADENA).Truncar(tamaño);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoIgualALargoDeCadena_ResultadoOk()
        {
            ushort tamaño = (ushort)CADENA.Length;
            
            string res = CADENA.Truncar(tamaño);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoMayorALargoDeCadenaYSufijo_ResultadoOk()
        {
            ushort tamaño = (ushort)(CADENA.Length + 5);

            string res = CADENA.Truncar(tamaño, 
                sufijo: SUFIJO_TRUNCADO);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoMenorALargoDeCadenaYSufijo_ResultadoOk()
        {
            ushort tamaño = (ushort)(CADENA.Length);

            string res = (CADENA + CADENA).Truncar(tamaño,
                sufijo: SUFIJO_TRUNCADO);

            Assert.AreEqual(
                expected: CADENA + SUFIJO_TRUNCADO,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoIgualALargoDeCadenaYSufijo_ResultadoOk()
        {
            ushort tamaño = (ushort)CADENA.Length;

            string res = CADENA.Truncar(tamaño,
                sufijo: SUFIJO_TRUNCADO);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        #endregion

        #region CrearTextoDesdeDiccionario

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void CrearTextoDesdeDiccionario_DiccionarioNull_ArrojaExcepcion()
        {
            Dictionary<string, string> diccionario = null;
            diccionario.CrearTextoDesdeDiccionario();
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioVacio_DevuelveCadenaVacia()
        {
            var diccionario = new Dictionary<string, string>();

            string res = diccionario.CrearTextoDesdeDiccionario();

            Assert.AreEqual(
                expected: String.Empty,
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioCon1Elemento_ResultadoOk()
        {
            string clave = "A",
                valor = "123";

            var diccionario = new Dictionary<string, string>
            {
                { clave, valor }
            };

            string res = diccionario.CrearTextoDesdeDiccionario();

            Assert.AreEqual(
                expected: clave + ": " + valor,
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioCon3Elementos_ResultadoOk()
        {
            string clave1 = "A",
                clave2 = "B",
                clave3 = "C",
                valor1 = "123",
                valor2 = "456",
                valor3 = "789";

            var diccionario = new Dictionary<string, string>
            {
                { clave1, valor1 },
                { clave2, valor2 },
                { clave3, valor3 }
            };

            string res = diccionario.CrearTextoDesdeDiccionario();

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(SEPARADOR_CLAVE_VALOR, SEPARADOR_ITEM, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioCon3ElementosConSeparadorClaveValorEspecificado_ResultadoOk()
        {
            string clave1 = "A",
                clave2 = "B",
                clave3 = "C",
                valor1 = "123",
                valor2 = "456",
                valor3 = "789",
                separadorClaveValor = ")";

            var diccionario = new Dictionary<string, string>
            {
                { clave1, valor1 },
                { clave2, valor2 },
                { clave3, valor3 }
            };

            string res = diccionario.CrearTextoDesdeDiccionario(
                separadorClaveValor: separadorClaveValor);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, SEPARADOR_ITEM, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioCon3ElementosConSeparadorClaveValorYSeparadorItemEspecificados_ResultadoOk()
        {
            string clave1 = "A",
                clave2 = "B",
                clave3 = "C",
                valor1 = "123",
                valor2 = "456",
                valor3 = "789",
                separadorClaveValor = ")",
                separadorItem = ". ";

            var diccionario = new Dictionary<string, string>
            {
                { clave1, valor1 },
                { clave2, valor2 },
                { clave3, valor3 }
            };

            string res = diccionario.CrearTextoDesdeDiccionario(
                separadorClaveValor: separadorClaveValor,
                separadorItemLista: separadorItem);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, separadorItem, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioCon3ElementosConSeparadorClaveValorNull_ResultadoOk()
        {
            string clave1 = "A",
                clave2 = "B",
                clave3 = "C",
                valor1 = "123",
                valor2 = "456",
                valor3 = "789",
                separadorClaveValor = null;

            var diccionario = new Dictionary<string, string>
            {
                { clave1, valor1 },
                { clave2, valor2 },
                { clave3, valor3 }
            };

            string res = diccionario.CrearTextoDesdeDiccionario(
                separadorClaveValor: separadorClaveValor);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, SEPARADOR_ITEM, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioCon3ElementosConSeparadorClaveValorYSeparadorItemNull_ResultadoOk()
        {
            string clave1 = "A",
                clave2 = "B",
                clave3 = "C",
                valor1 = "123",
                valor2 = "456",
                valor3 = "789",
                separadorClaveValor = null,
                separadorItem = null;

            var diccionario = new Dictionary<string, string>
            {
                { clave1, valor1 },
                { clave2, valor2 },
                { clave3, valor3 }
            };

            string res = diccionario.CrearTextoDesdeDiccionario(
                separadorClaveValor: separadorClaveValor,
                separadorItemLista: separadorItem);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, separadorItem, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: res);
        }

        private string GenerarTextoDiccionario<TClave, TValor>(string separadorClaveValor, string separadorItem, TClave clave1, TValor valor1, TClave clave2, TValor valor2, TClave clave3, TValor valor3) 
        {
            return String.Format("{2}{0}{3}{1}{4}{0}{5}{1}{6}{0}{7}",
                separadorClaveValor, separadorItem,
                clave1, valor1, clave2, valor2, clave3, valor3);
        }

        #endregion

        #region CrearTextoDesdeLista

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void CrearTextoDesdeLista_ListaNull_ArrojaExcepcion()
        {
            List<string> lista = null;
            lista.CrearTextoDesdeLista();
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaVacia_DevuelveCadenaVacia()
        {
            var lista = new List<string>();

            string res = lista.CrearTextoDesdeLista();

            Assert.AreEqual(
                expected: String.Empty,
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon1Elemento_ResultadoOk()
        {
            string valor = "123";

            var lista = new List<string> { valor };

            string res = lista.CrearTextoDesdeLista();

            Assert.AreEqual(
                expected: valor,
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon3Elementos_ResultadoOk()
        {
            string valor1 = "123",
                valor2 = "456",
                valor3 = "789";

            var lista = new List<string> { valor1, valor2, valor3 };

            string res = lista.CrearTextoDesdeLista();

            Assert.AreEqual(
                expected: GenerarTextoLista(SEPARADOR_ITEM, valor1, valor2, valor3),
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon3ElementosConSeparadorEspecificado_ResultadoOk()
        {
            string valor1 = "123",
                valor2 = "456",
                valor3 = "789",
                separador = ". ";

            var lista = new List<string> { valor1, valor2, valor3 };

            string res = lista.CrearTextoDesdeLista(
                separador: separador);

            Assert.AreEqual(
                expected: GenerarTextoLista(separador, valor1, valor2, valor3),
                actual: res);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon3ElementosVaciosYUnoConValor_DevuelveTextoConUnoSolo()
        {
            string valor1 = "123",
                valor2 = "",
                valor3 = null,
                valor4 = "   ";

            var lista = new List<string> { valor1, valor2, valor3, valor4 };

            string res = lista.CrearTextoDesdeLista(
                eliminarVacios: true);

            Assert.AreEqual(
                expected: valor1,
                actual: res);
        }

        private string GenerarTextoLista<T>(string separadorItem, T valor1, T valor2, T valor3)
        {
            return String.Format("{1}{0}{2}{0}{3}",
                separadorItem,
                valor1, valor2, valor3);
        }

        #endregion

        #region Capitalizar

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void Capitalizar_ValorNull_ArrojaExcepcion()
        {
            string stringNull = null;
            stringNull.Capitalizar();
        }

        [TestMethod]
        public void Capitalizar_ValorCadenaVacia_DevuelveCadenaVacia()
        {
            string res = String.Empty.Capitalizar();

            Assert.AreEqual(
                expected: String.Empty,
                actual: res);
        }

        [TestMethod]
        public void Capitalizar_ValorCadenaSinCapitalizacion_ResultadoOk()
        {
            string res = CADENA_SIN_CAPITALIZACION.Capitalizar();

            Assert.AreEqual(
                expected: CADENA_CON_CAPITALIZACION,
                actual: res);
        }

        [TestMethod]
        public void Capitalizar_ValorCadenaConCapitalizacion_DevuelveMismaCadena()
        {
            string res = CADENA_CON_CAPITALIZACION.Capitalizar();

            Assert.AreEqual(
                expected: CADENA_CON_CAPITALIZACION,
                actual: res);
        }

        #endregion
    }
}
