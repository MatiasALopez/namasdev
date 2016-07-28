using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using namasdev.Tipos;
using System.Collections.Generic;

namespace namasdev.Core.UnitTests.Tipos
{
    [TestClass]
    public class StringUtilidadesTest
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
            string resNull = StringUtilidades.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes(null);
            string resCadenaVacia = StringUtilidades.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes(String.Empty);
            string resEspacios = StringUtilidades.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes("    ");

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
            string res = StringUtilidades.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes(CADENA_CON_ACENTOS_Y_ESPECIALES);

            Assert.AreEqual(
                expected: CADENA_SIN_ACENTOS_Y_ESPECIALES,
                actual: res);
        }

        [TestMethod]
        public void ReemplazarAcentosYCaracteresEspecialesPorEquivalentes_ValorSinAcentosYCaracteresEspeciales_ResultadoOk()
        {
            string res = StringUtilidades.ReemplazarAcentosYCaracteresEspecialesPorEquivalentes(CADENA_SIN_ACENTOS_Y_ESPECIALES);

            Assert.AreEqual(
                expected: CADENA_SIN_ACENTOS_Y_ESPECIALES,
                actual: res);
        }

        #endregion

        #region QuitarCaracteresNoAlfanumericos

        [TestMethod]
        public void QuitarCaracteresNoAlfanumericos_ValorNullOCadenaVaciaOEspacios_DevuelveCadenaVacia()
        {
            string resNull = StringUtilidades.QuitarCaracteresNoAlfanumericos(null);
            string resCadenaVacia = StringUtilidades.QuitarCaracteresNoAlfanumericos(String.Empty);
            string resEspacios = StringUtilidades.QuitarCaracteresNoAlfanumericos("    ");

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
            string res = StringUtilidades.QuitarCaracteresNoAlfanumericos(CADENA_CON_CARACTERES_NO_ALFANUMERICOS);

            Assert.AreEqual(
                expected: CADENA_SIN_CARACTERES_NO_ALFANUMERICOS,
                actual: res);
        }

        [TestMethod]
        public void QuitarCaracteresNoAlfanumericos_ValorSinCaracteresNoAlfanumericos_ResultadoOk()
        {
            string res = StringUtilidades.QuitarCaracteresNoAlfanumericos(CADENA_SIN_CARACTERES_NO_ALFANUMERICOS);

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
            string resNull = StringUtilidades.Truncar(null, tamaño);
            string resCadenaVacia = StringUtilidades.Truncar(String.Empty, tamaño);
            string resEspacios = StringUtilidades.Truncar("    ", tamaño);

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

            string res = StringUtilidades.Truncar(CADENA, tamaño);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoMenorALargoDeCadena_ResultadoOk()
        {
            ushort tamaño = (ushort)(CADENA.Length);

            string res = StringUtilidades.Truncar(CADENA + CADENA, tamaño);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoIgualALargoDeCadena_ResultadoOk()
        {
            ushort tamaño = (ushort)CADENA.Length;
            
            string res = StringUtilidades.Truncar(CADENA, tamaño);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoMayorALargoDeCadenaYSufijo_ResultadoOk()
        {
            ushort tamaño = (ushort)(CADENA.Length + 5);

            string res = StringUtilidades.Truncar(CADENA, tamaño, 
                sufijo: SUFIJO_TRUNCADO);

            Assert.AreEqual(
                expected: CADENA,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoMenorALargoDeCadenaYSufijo_ResultadoOk()
        {
            ushort tamaño = (ushort)(CADENA.Length);

            string res = StringUtilidades.Truncar(CADENA + CADENA, tamaño,
                sufijo: SUFIJO_TRUNCADO);

            Assert.AreEqual(
                expected: CADENA + SUFIJO_TRUNCADO,
                actual: res);
        }

        [TestMethod]
        public void Truncar_TamañoIgualALargoDeCadenaYSufijo_ResultadoOk()
        {
            ushort tamaño = (ushort)CADENA.Length;

            string res = StringUtilidades.Truncar(CADENA, tamaño,
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
            StringUtilidades.CrearTextoDesdeDiccionario(diccionario);
        }

        [TestMethod]
        public void CrearTextoDesdeDiccionario_DiccionarioVacio_DevuelveCadenaVacia()
        {
            var diccionario = new Dictionary<string, string>();

            string texto = StringUtilidades.CrearTextoDesdeDiccionario(diccionario);

            Assert.AreEqual(
                expected: String.Empty,
                actual: texto);
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

            string texto = StringUtilidades.CrearTextoDesdeDiccionario(diccionario);

            Assert.AreEqual(
                expected: clave + ": " + valor,
                actual: texto);
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

            string texto = StringUtilidades.CrearTextoDesdeDiccionario(diccionario);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(SEPARADOR_CLAVE_VALOR, SEPARADOR_ITEM, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: texto);
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

            string texto = StringUtilidades.CrearTextoDesdeDiccionario(diccionario,
                separadorClaveValor: separadorClaveValor);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, SEPARADOR_ITEM, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: texto);
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

            string texto = StringUtilidades.CrearTextoDesdeDiccionario(diccionario,
                separadorClaveValor: separadorClaveValor,
                separadorItemLista: separadorItem);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, separadorItem, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: texto);
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

            string texto = StringUtilidades.CrearTextoDesdeDiccionario(diccionario,
                separadorClaveValor: separadorClaveValor);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, SEPARADOR_ITEM, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: texto);
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

            string texto = StringUtilidades.CrearTextoDesdeDiccionario(diccionario,
                separadorClaveValor: separadorClaveValor,
                separadorItemLista: separadorItem);

            Assert.AreEqual(
                expected: GenerarTextoDiccionario(separadorClaveValor, separadorItem, clave1, valor1, clave2, valor2, clave3, valor3),
                actual: texto);
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
            StringUtilidades.CrearTextoDesdeLista(lista);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaVacia_DevuelveCadenaVacia()
        {
            var lista = new List<string>();

            string texto = StringUtilidades.CrearTextoDesdeLista(lista);

            Assert.AreEqual(
                expected: String.Empty,
                actual: texto);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon1Elemento_ResultadoOk()
        {
            string valor = "123";

            var lista = new List<string> { valor };

            string texto = StringUtilidades.CrearTextoDesdeLista(lista);

            Assert.AreEqual(
                expected: valor,
                actual: texto);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon3Elementos_ResultadoOk()
        {
            string valor1 = "123",
                valor2 = "456",
                valor3 = "789";

            var lista = new List<string> { valor1, valor2, valor3 };

            string texto = StringUtilidades.CrearTextoDesdeLista(lista);

            Assert.AreEqual(
                expected: GenerarTextoLista(SEPARADOR_ITEM, valor1, valor2, valor3),
                actual: texto);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon3ElementosConSeparadorEspecificado_ResultadoOk()
        {
            string valor1 = "123",
                valor2 = "456",
                valor3 = "789",
                separador = ". ";

            var lista = new List<string> { valor1, valor2, valor3 };

            string texto = StringUtilidades.CrearTextoDesdeLista(lista,
                separador: separador);

            Assert.AreEqual(
                expected: GenerarTextoLista(separador, valor1, valor2, valor3),
                actual: texto);
        }

        [TestMethod]
        public void CrearTextoDesdeLista_ListaCon3ElementosVaciosYUnoConValor_DevuelveTextoConUnoSolo()
        {
            string valor1 = "123",
                valor2 = "",
                valor3 = null,
                valor4 = "   ";

            var lista = new List<string> { valor1, valor2, valor3, valor4 };

            string texto = StringUtilidades.CrearTextoDesdeLista(lista, 
                eliminarVacios: true);

            Assert.AreEqual(
                expected: valor1,
                actual: texto);
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
            StringUtilidades.Capitalizar(null);
        }

        [TestMethod]
        public void Capitalizar_ValorCadenaVacia_DevuelveCadenaVacia()
        {
            string resultado = StringUtilidades.Capitalizar(String.Empty);

            Assert.AreEqual(
                expected: String.Empty,
                actual: resultado);
        }

        [TestMethod]
        public void Capitalizar_ValorCadenaSinCapitalizacion_ResultadoOk()
        {
            string resultado = StringUtilidades.Capitalizar(CADENA_SIN_CAPITALIZACION);

            Assert.AreEqual(
                expected: CADENA_CON_CAPITALIZACION,
                actual: resultado);
        }

        [TestMethod]
        public void Capitalizar_ValorCadenaConCapitalizacion_DevuelveMismaCadena()
        {
            string resultado = StringUtilidades.Capitalizar(CADENA_CON_CAPITALIZACION);

            Assert.AreEqual(
                expected: CADENA_CON_CAPITALIZACION,
                actual: resultado);
        }

        #endregion
    }
}
