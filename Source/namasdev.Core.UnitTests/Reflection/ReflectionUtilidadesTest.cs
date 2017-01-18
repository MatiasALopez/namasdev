using System;
using System.ComponentModel;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using namasdev.Reflection;
using namasdev.Core.UnitTests._TestUtils;

using FizzWare.NBuilder;

namespace namasdev.Core.UnitTests.Reflection
{
    [TestClass]
    public class ReflectionUtilidadesTest
    {
        #region ObtenerValorDefault (Generic)

        [TestMethod]
        public void ObtenerValorDefaultGeneric_TipoPorValor_ResultadoOk()
        {
            object resInt = ReflectionUtilidades.ObtenerValorDefault<int>();
            object resString = ReflectionUtilidades.ObtenerValorDefault<string>();
            object resDateTime = ReflectionUtilidades.ObtenerValorDefault<DateTime>();
            object resBool = ReflectionUtilidades.ObtenerValorDefault<bool>();
            object resDecimal = ReflectionUtilidades.ObtenerValorDefault<decimal>();

            Assert.AreEqual(
                expected: default(int),
                actual: resInt,
                message: "int");

            Assert.AreEqual(
                expected: default(string),
                actual: resString,
                message: "string");

            Assert.AreEqual(
                expected: default(DateTime),
                actual: resDateTime,
                message: "DateTime");

            Assert.AreEqual(
                expected: default(bool),
                actual: resBool,
                message: "bool");

            Assert.AreEqual(
                expected: default(decimal),
                actual: resDecimal,
                message: "decimal");
        }

        [TestMethod]
        public void ObtenerValorDefaultGeneric_TipoPorReferencia_ResultadoOk()
        {
            object resultado = ReflectionUtilidades.ObtenerValorDefault<Objeto>();

            Assert.AreEqual(
                expected: null,
                actual: resultado);
        }

        #endregion

        #region ObtenerValorDefault

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerValorDefault_TipoNull_ArrojaException()
        {
            Type tipo = null;

            ReflectionUtilidades.ObtenerValorDefault(tipo);
        }

        [TestMethod]
        public void ObtenerValorDefault_TipoPorValor_ResultadoOk()
        {
            Type tipoInt = typeof(int);
            Type tipoString = typeof(string);
            Type tipoDateTime = typeof(DateTime);
            Type tipoBool = typeof(bool);
            Type tipoDecimal = typeof(decimal);

            object resInt = ReflectionUtilidades.ObtenerValorDefault(tipoInt);
            object resString = ReflectionUtilidades.ObtenerValorDefault(tipoString);
            object resDateTime = ReflectionUtilidades.ObtenerValorDefault(tipoDateTime);
            object resBool = ReflectionUtilidades.ObtenerValorDefault(tipoBool);
            object resDecimal = ReflectionUtilidades.ObtenerValorDefault(tipoDecimal);

            Assert.AreEqual(
                expected: default(int),
                actual: resInt,
                message: "int");

            Assert.AreEqual(
                expected: default(string),
                actual: resString,
                message: "string");

            Assert.AreEqual(
                expected: default(DateTime),
                actual: resDateTime,
                message: "DateTime");

            Assert.AreEqual(
                expected: default(bool),
                actual: resBool,
                message: "bool");

            Assert.AreEqual(
                expected: default(decimal),
                actual: resDecimal,
                message: "decimal");
        }

        [TestMethod]
        public void ObtenerValorDefault_TipoPorReferencia_ResultadoOk()
        {
            Type tipo = typeof(Objeto);

            object resultado = ReflectionUtilidades.ObtenerValorDefault(tipo);

            Assert.AreEqual(
                expected: null,
                actual: resultado);
        }

        #endregion

        #region ObtenerPropiedades (Generic)

        [TestMethod]
        public void ObtenerPropiedadesGeneric_ResultadoOk()
        {
            string[] nombresPropiedadesEsperadas = { "Texto", "Entero", "Guid", "FechaHora" };

            var res = ReflectionUtilidades.ObtenerPropiedades<Objeto>();
            var nombresPropiedadesEncontradas = res.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(nombresPropiedadesEsperadas, nombresPropiedadesEncontradas);
        }

        #endregion

        #region ObtenerPropiedades

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerPropiedades_TipoNull_ArrojaException()
        {
            Type tipo = null;

            ReflectionUtilidades.ObtenerPropiedades(tipo);
        }

        [TestMethod]
        public void ObtenerPropiedades_ResultadoOk()
        {
            Type tipo = typeof(Objeto);

            string[] nombresPropiedadesEsperadas = { "Texto", "Entero", "Guid", "FechaHora" };

            var res = ReflectionUtilidades.ObtenerPropiedades(tipo);
            var nombresPropiedadesEncontradas = res.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(nombresPropiedadesEsperadas, nombresPropiedadesEncontradas);
        }

        #endregion

        #region ObtenerPropiedadesDeTipo (Generic)

        [TestMethod]
        public void ObtenerPropiedadesDeTipoGeneric_TiposPropiedadesInexistentes_DevuelveArrayVacio()
        {
            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipo<Objeto, double>();

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoGeneric_TiposPropiedadesExistentes_ResultadoOk()
        {
            string[] nombresPropiedadesEsperadasString = { "Texto" };
            string[] nombresPropiedadesEsperadasDateTime = { "FechaHora" };

            var resString = ReflectionUtilidades.ObtenerPropiedadesDeTipo<Objeto, string>();
            var resDateTime = ReflectionUtilidades.ObtenerPropiedadesDeTipo<Objeto, DateTime?>();

            var nombresPropiedadesEncontradasString = resString.Select(p => p.Name).ToArray();
            var nombresPropiedadesEncontradasDateTime = resDateTime.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasString,
                actual: nombresPropiedadesEncontradasString);

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasDateTime,
                actual: nombresPropiedadesEncontradasDateTime);
        }

        #endregion

        #region ObtenerPropiedadesDeTipo

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerPropiedadesDeTipo_TipoClaseNull_ArrojaException()
        {
            Type tipoClase = null;
            Type tipoPropiedad = typeof(string);

            ReflectionUtilidades.ObtenerPropiedadesDeTipo(tipoClase, tipoPropiedad);
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerPropiedadesDeTipo_TipoPropiedadNull_ArrojaException()
        {
            Type tipoClase = typeof(string);
            Type tipoPropiedad = null;

            ReflectionUtilidades.ObtenerPropiedadesDeTipo(tipoClase, tipoPropiedad);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipo_TiposPropiedadesInexistentes_DevuelveArrayVacio()
        {
            Type tipoClase = typeof(Objeto);

            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipo(tipoClase, typeof(double));

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipo_TiposPropiedadesExistentes_ResultadoOk()
        {
            Type tipo = typeof(Objeto);

            string[] nombresPropiedadesEsperadasString = { "Texto" };
            string[] nombresPropiedadesEsperadasDateTime = { "FechaHora" };

            var resString = ReflectionUtilidades.ObtenerPropiedadesDeTipo(tipo, typeof(string));
            var resDateTime = ReflectionUtilidades.ObtenerPropiedadesDeTipo(tipo, typeof(DateTime?));

            var nombresPropiedadesEncontradasString = resString.Select(p => p.Name).ToArray();
            var nombresPropiedadesEncontradasDateTime = resDateTime.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasString,
                actual: nombresPropiedadesEncontradasString);

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasDateTime,
                actual: nombresPropiedadesEncontradasDateTime);
        }

        #endregion

        #region LasPropiedadesDeLosObjetosSonIguales

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void LasPropiedadesDeLosObjetosSonIguales_Objeto1Null_ArrojaException()
        {
            Objeto obj1 = null,
                obj2 = new Objeto();

            ReflectionUtilidades.LasPropiedadesDeLosObjetosSonIguales(obj1, obj2);
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void LasPropiedadesDeLosObjetosSonIguales_Objeto2Null_ArrojaException()
        {
            Objeto obj1 = new Objeto(),
                obj2 = null;

            ReflectionUtilidades.LasPropiedadesDeLosObjetosSonIguales(obj1, obj2);
        }

        [TestMethod]
        public void LasPropiedadesDeLosObjetosSonIguales_MismoObjeto_DevuelveTrue()
        {
            Objeto obj1 = new Objeto(),
                obj2 = obj1;

            Assert.IsTrue(ReflectionUtilidades.LasPropiedadesDeLosObjetosSonIguales(obj1, obj2));
        }

        [TestMethod]
        public void LasPropiedadesDeLosObjetosSonIguales_MismasPropiedades1_DevuelveTrue()
        {
            Objeto obj1 = new Objeto(),
                obj2 = new Objeto();

            Assert.IsTrue(ReflectionUtilidades.LasPropiedadesDeLosObjetosSonIguales(obj1, obj2));
        }

        [TestMethod]
        public void LasPropiedadesDeLosObjetosSonIguales_MismasPropiedades2_DevuelveTrue()
        {
            Objeto obj1 = Builder<Objeto>.CreateNew().Build(),
                obj2 = new Objeto { Texto = obj1.Texto, Entero = obj1.Entero, FechaHora = obj1.FechaHora, Guid = obj1.Guid };

            Assert.IsTrue(ReflectionUtilidades.LasPropiedadesDeLosObjetosSonIguales(obj1, obj2));
        }

        [TestMethod]
        public void LasPropiedadesDeLosObjetosSonIguales_PropiedadesConValoresDiferentes_DevuelveFalse()
        {
            var objetos = Builder<Objeto>.CreateListOfSize(2).Build();
            
            Assert.IsFalse(ReflectionUtilidades.LasPropiedadesDeLosObjetosSonIguales(objetos[0], objetos[1]));
        }

        #endregion

        #region CrearInstancia

        [TestMethod,
        ExpectedException(typeof(MissingMethodException))]
        public void CrearInstancia_ParametrosNoValidosParaUnObjeto_ArrojaException()
        {
            ReflectionUtilidades.CrearInstancia<Objeto>("texto");
        }

        [TestMethod]
        public void CrearInstancia_ConstructorSinParametros_ResultadoOk()
        {
            var obj1 = ReflectionUtilidades.CrearInstancia<Objeto>();
            var obj2 = new Objeto();

            Assert.IsTrue(obj1.Equals(obj2));
        }

        [TestMethod]
        public void CrearInstancia_ConstructorConParametros_ResultadoOk()
        {
            string texto = "texto";
            int entero = 10;
            Guid guid = Guid.NewGuid();
            DateTime fechaHora = DateTime.Now;

            var obj1 = ReflectionUtilidades.CrearInstancia<Objeto>(texto, entero, guid, fechaHora);
            var obj2 = new Objeto(texto, entero, guid, fechaHora);

            Assert.IsTrue(obj1.Equals(obj2));
        }

        #endregion

        #region TodasLasPropiedadesDelObjetoTienenValorNull

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void TodasLasPropiedadesDelObjetoTienenValorNull_ObjetoNull_ArrojaException()
        {
            Objeto obj = null;
            ReflectionUtilidades.TodasLasPropiedadesDelObjetoTienenValorNull(obj);
        }

        [TestMethod]
        public void TodasLasPropiedadesDelObjetoTienenValorNull_ObjetoConTodosNull_DevuelveTrue()
        {
            var obj = new Objeto();

            bool resultado = ReflectionUtilidades.TodasLasPropiedadesDelObjetoTienenValorNull(obj);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void TodasLasPropiedadesDelObjetoTienenValorNull_ObjetoConValoresDefault_DevuelveFalse()
        {
            var obj = new Objeto { Texto = default(string), Entero = default(int), Guid = default(Guid), FechaHora = default(DateTime) };

            bool resultado = ReflectionUtilidades.TodasLasPropiedadesDelObjetoTienenValorNull(obj);

            Assert.IsFalse(resultado);
        }

        #endregion

        #region TodasLasPropiedadesDelObjetoTienenValorDefault

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void TodasLasPropiedadesDelObjetoTienenValorDefault_ObjetoNull_ArrojaException()
        {
            Objeto obj = null;
            ReflectionUtilidades.TodasLasPropiedadesDelObjetoTienenValorDefault(obj);
        }

        [TestMethod]
        public void TodasLasPropiedadesDelObjetoTienenValorDefault_ObjetoConValoresDefault_DevuelveTrue()
        {
            var obj = new ObjetoConPropiedadesNoNullables { Texto = default(string), Entero = default(int), Guid = default(Guid), FechaHora = default(DateTime) };

            bool resultado = ReflectionUtilidades.TodasLasPropiedadesDelObjetoTienenValorDefault(obj);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void TodasLasPropiedadesDelObjetoTienenValorDefault_ObjetoConValoresNoDefault_DevuelveTrue()
        {
            var obj = Builder<Objeto>.CreateNew().Build();

            bool resultado = ReflectionUtilidades.TodasLasPropiedadesDelObjetoTienenValorDefault(obj);

            Assert.IsFalse(resultado);
        }

        #endregion

        #region ObtenerAtributoDeCampo

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerAtributoDeCampo_TipoNull_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDeCampo<System.ComponentModel.DescriptionAttribute>(null, "Campo");
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerAtributoDeCampo_NombreCampoNull_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDeCampo<System.ComponentModel.DescriptionAttribute>(typeof(Objeto), null);
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerAtributoDeCampo_NombreCampoVacio_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDeCampo<System.ComponentModel.DescriptionAttribute>(typeof(Objeto), String.Empty);
        }

        [TestMethod,
        ExpectedException(typeof(MissingMemberException))]
        public void ObtenerAtributoDeCampo_NombreDeCampoInexistente_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDeCampo<System.ComponentModel.DescriptionAttribute>(typeof(Objeto), "Inexistente");
        }

        [TestMethod]
        public void ObtenerAtributoDeCampo_TipoYNombreDePropiedadDeObjeto_ResultadoOk()
        {
            Type tipo = typeof(Objeto);
            string nombreCampo = "CampoTexto";

            var resAtributoDesc = ReflectionUtilidades.ObtenerAtributoDeCampo<System.ComponentModel.DescriptionAttribute>(tipo, nombreCampo);
            var resAtributoCat = ReflectionUtilidades.ObtenerAtributoDeCampo<CategoryAttribute>(tipo, nombreCampo);

            var esperadoAtributoDesc = new System.ComponentModel.DescriptionAttribute(Objeto.TEXTO_DESCRIPCION);
            var esperadoAtributoCat = new CategoryAttribute(Objeto.TEXTO_CATEGORIA);

            Assert.AreEqual(
                expected: esperadoAtributoDesc.Description,
                actual: resAtributoDesc.Description,
                message: "Description");

            Assert.AreEqual(
                expected: esperadoAtributoCat.Category,
                actual: resAtributoCat.Category,
                message: "Category");
        }

        [TestMethod]
        public void ObtenerAtributoDeCampo_TipoYNombreDeEnum_ResultadoOk()
        {
            Type tipo = typeof(Enumeracion);
            string nombreCampo = Enumeracion.Opcion1.ToString();

            var res = ReflectionUtilidades.ObtenerAtributoDeCampo<System.ComponentModel.DescriptionAttribute>(tipo, nombreCampo);

            var esperado = new System.ComponentModel.DescriptionAttribute("Opción 1");

            Assert.AreEqual(
                expected: esperado,
                actual: res);
        }

        #endregion

        #region ObtenerAtributoDePropiedad

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerAtributoDePropiedad_TipoNull_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDePropiedad<System.ComponentModel.DescriptionAttribute>(null, "Propiedad");
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerAtributoDePropiedad_NombrePropiedadNull_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDePropiedad<System.ComponentModel.DescriptionAttribute>(typeof(Objeto), null);
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerAtributoDePropiedad_NombrePropiedadVacio_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDePropiedad<System.ComponentModel.DescriptionAttribute>(typeof(Objeto), String.Empty);
        }

        [TestMethod,
        ExpectedException(typeof(MissingMemberException))]
        public void ObtenerAtributoDePropiedad_NombreDePropiedadInexistente_ArrojaException()
        {
            ReflectionUtilidades.ObtenerAtributoDePropiedad<System.ComponentModel.DescriptionAttribute>(typeof(Objeto), "Inexistente");
        }

        [TestMethod]
        public void ObtenerAtributoDePropiedad_TipoYNombreDePropiedadDeObjeto_ResultadoOk()
        {
            Type tipo = typeof(Objeto);
            string nombrePropiedad = "Texto";

            var resAtributoDesc = ReflectionUtilidades.ObtenerAtributoDePropiedad<System.ComponentModel.DescriptionAttribute>(tipo, nombrePropiedad);
            var resAtributoCat = ReflectionUtilidades.ObtenerAtributoDePropiedad<CategoryAttribute>(tipo, nombrePropiedad);

            var esperadoAtributoDesc = new System.ComponentModel.DescriptionAttribute(Objeto.TEXTO_DESCRIPCION);
            var esperadoAtributoCat = new CategoryAttribute(Objeto.TEXTO_CATEGORIA);

            Assert.AreEqual(
                expected: esperadoAtributoDesc.Description,
                actual: resAtributoDesc.Description,
                message: "Description");

            Assert.AreEqual(
                expected: esperadoAtributoCat.Category,
                actual: resAtributoCat.Category,
                message: "Category");
        }

        [TestMethod]
        public void ObtenerAtributoDePropiedad_TipoYNombreDePropiedadDeObjetoDerivado_ResultadoOk()
        {
            Type tipo = typeof(ObjetoDerivado);
            string nombrePropiedad = "Texto";

            var resAtributoDesc = ReflectionUtilidades.ObtenerAtributoDePropiedad<System.ComponentModel.DescriptionAttribute>(tipo, nombrePropiedad);
            var resAtributoCat = ReflectionUtilidades.ObtenerAtributoDePropiedad<CategoryAttribute>(tipo, nombrePropiedad);

            var esperadoAtributoDesc = new System.ComponentModel.DescriptionAttribute(Objeto.TEXTO_DESCRIPCION);
            var esperadoAtributoCat = new CategoryAttribute(Objeto.TEXTO_CATEGORIA);

            Assert.AreEqual(
                expected: esperadoAtributoDesc.Description,
                actual: resAtributoDesc.Description,
                message: "Description");

            Assert.AreEqual(
                expected: esperadoAtributoCat.Category,
                actual: resAtributoCat.Category,
                message: "Category");
        }

        #endregion

        #region ObtenerPropiedadesConAtributo (Generic)
        
        [TestMethod]
        public void ObtenerPropiedadesConAtributoGeneric_NingunaPropiedadPoseeAtributo_DevuelveArrayVacio()
        {
            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<Objeto, System.ComponentModel.DataAnnotations.PhoneAttribute>();

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesConAtributoGeneric_PropiedadesPoseenAtributo_ResultadoOk()
        {
            string[] nombresPropiedadesEsperadas = { "Entero", "FechaHora" };

            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<Objeto, System.ComponentModel.DataAnnotations.RequiredAttribute>();

            var nombresPropiedadesEncontradas = res.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadas,
                actual: nombresPropiedadesEncontradas);
        }

        [TestMethod]
        public void ObtenerPropiedadesConAtributoGeneric_PropiedadesPoseenAtributoQueNoCumpleCondiciones_DevuelveArrayVacio()
        {
            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<Objeto, System.ComponentModel.CategoryAttribute>(
                condicionAtributo: (a) => a.Category == "Inexistente");

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesConAtributoGeneric_PropiedadesPoseenAtributoQueCumpleCondiciones_ResultadoOk()
        {
            string[] nombresPropiedadesEsperadas = { "Texto" };

            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<Objeto, System.ComponentModel.CategoryAttribute>(
                condicionAtributo: (a) => a.Category == Objeto.TEXTO_CATEGORIA);

            var nombresPropiedadesEncontradas = res.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadas,
                actual: nombresPropiedadesEncontradas);
        }

        #endregion

        #region ObtenerPropiedadesConAtributo

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerPropiedadesConAtributo_TipoClaseNull_ArrojaException()
        {
            Type tipoClase = null;

            ReflectionUtilidades.ObtenerPropiedadesConAtributo<System.ComponentModel.DescriptionAttribute>(tipoClase);
        }

        [TestMethod]
        public void ObtenerPropiedadesConAtributo_NingunaPropiedadPoseeAtributo_DevuelveArrayVacio()
        {
            Type tipoClase = typeof(Objeto);

            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<System.ComponentModel.DataAnnotations.PhoneAttribute>(tipoClase);

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesConAtributo_PropiedadesPoseenAtributo_ResultadoOk()
        {
            Type tipoClase = typeof(Objeto);
            string[] nombresPropiedadesEsperadas = { "Entero", "FechaHora" };

            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<System.ComponentModel.DataAnnotations.RequiredAttribute>(tipoClase);

            var nombresPropiedadesEncontradas = res.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadas,
                actual: nombresPropiedadesEncontradas);
        }

        [TestMethod]
        public void ObtenerPropiedadesConAtributo_PropiedadesPoseenAtributoQueNoCumpleCondiciones_DevuelveArrayVacio()
        {
            Type tipoClase = typeof(Objeto);

            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<System.ComponentModel.CategoryAttribute>(tipoClase,
                condicionAtributo: (a) => a.Category == "Inexistente");

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesConAtributo_PropiedadesPoseenAtributoQueCumpleCondiciones_ResultadoOk()
        {
            Type tipoClase = typeof(Objeto);

            string[] nombresPropiedadesEsperadas = { "Texto" };

            var res = ReflectionUtilidades.ObtenerPropiedadesConAtributo<System.ComponentModel.CategoryAttribute>(tipoClase,
                condicionAtributo: (a) => a.Category == Objeto.TEXTO_CATEGORIA);

            var nombresPropiedadesEncontradas = res.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadas,
                actual: nombresPropiedadesEncontradas);
        }

        #endregion

        #region ObtenerPropiedadesDeTipoConAtributo (Generic)

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributoGeneric_TiposPropiedadesInexistentes_DevuelveArrayVacio()
        {
            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<Objeto, double, System.ComponentModel.DescriptionAttribute>();

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributoGeneric_TiposPropiedadesExistentesNingunaPoseeAtributo_DevuelveArrayVacio()
        {
            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<Objeto, string, System.ComponentModel.DataAnnotations.PhoneAttribute>();

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributoGeneric_TiposPropiedadesExistentesPoseenAtributo_ResultadoOk()
        {
            string[] nombresPropiedadesEsperadasString = { "Texto" };
            string[] nombresPropiedadesEsperadasDateTime = { "FechaHora" };

            var resString = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<Objeto, string, System.ComponentModel.DescriptionAttribute>();
            var resDateTime = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<Objeto, DateTime?, System.ComponentModel.DataAnnotations.RequiredAttribute>();

            var nombresPropiedadesEncontradasString = resString.Select(p => p.Name).ToArray();
            var nombresPropiedadesEncontradasDateTime = resDateTime.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasString,
                actual: nombresPropiedadesEncontradasString);

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasDateTime,
                actual: nombresPropiedadesEncontradasDateTime);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributoGeneric_TiposPropiedadesExistentesPoseenAtributoQueNoCumpleCondiciones_DevuelveArrayVacio()
        {
            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<Objeto, string, System.ComponentModel.CategoryAttribute>(
                condicionAtributo: (a) => a.Category == "Inexistente");

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributoGeneric_TiposPropiedadesExistentesPoseenAtributoQueCumpleCondiciones_ResultadoOk()
        {
            string[] nombresPropiedadesEsperadas = { "Texto" };

            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<Objeto, string, System.ComponentModel.CategoryAttribute>(
                condicionAtributo: (a) => a.Category == Objeto.TEXTO_CATEGORIA);

            var nombresPropiedadesEncontradas = res.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadas,
                actual: nombresPropiedadesEncontradas);
        }

        #endregion

        #region ObtenerPropiedadesDeTipo

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerPropiedadesDeTipoConAtributo_TipoClaseNull_ArrojaException()
        {
            Type tipoClase = null;
            Type tipoPropiedad = typeof(string);

            ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DescriptionAttribute>(tipoClase, tipoPropiedad);
        }

        [TestMethod,
        ExpectedException(typeof(ArgumentNullException))]
        public void ObtenerPropiedadesDeTipoConAtributo_TipoPropiedadNull_ArrojaException()
        {
            Type tipoClase = typeof(string);
            Type tipoPropiedad = null;

            ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DescriptionAttribute>(tipoClase, tipoPropiedad);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributo_TiposPropiedadesInexistentes_DevuelveArrayVacio()
        {
            Type tipoClase = typeof(Objeto);
            Type tipoPropiedad = typeof(double);

            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DescriptionAttribute>(tipoClase, tipoPropiedad);

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributo_TiposPropiedadesExistentesNingunaPoseeAtributo_DevuelveArrayVacio()
        {
            Type tipoClase = typeof(Objeto);
            Type tipoPropiedad = typeof(string);

            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DataAnnotations.PhoneAttribute>(tipoClase, tipoPropiedad);

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributo_TiposPropiedadesExistentesPoseenAtributo_ResultadoOk()
        {
            Type tipoClase = typeof(Objeto);

            Type tipoPropiedadString = typeof(string);
            Type tipoPropiedadDateTime = typeof(DateTime?);

            string[] nombresPropiedadesEsperadasString = { "Texto" };
            string[] nombresPropiedadesEsperadasDateTime = { "FechaHora" };

            var resString = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DescriptionAttribute>(tipoClase, tipoPropiedadString);
            var resDateTime = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DataAnnotations.RequiredAttribute>(tipoClase, tipoPropiedadDateTime);

            var nombresPropiedadesEncontradasString = resString.Select(p => p.Name).ToArray();
            var nombresPropiedadesEncontradasDateTime = resDateTime.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasString,
                actual: nombresPropiedadesEncontradasString);

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasDateTime,
                actual: nombresPropiedadesEncontradasDateTime);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributo_TiposPropiedadesExistentesPoseenAtributoQueNoCumpleCondiciones_DevuelveArrayVacio()
        {
            Type tipoClase = typeof(Objeto);
            Type tipoPropiedad = typeof(string);

            var res = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.CategoryAttribute>(tipoClase, tipoPropiedad,
                condicionAtributo: (a) => a.Category == "Inexistente");

            Assert.AreEqual(
                expected: 0,
                actual: res.Length);
        }

        [TestMethod]
        public void ObtenerPropiedadesDeTipoConAtributo_TiposPropiedadesExistentesPoseenAtributoQueCumpleCondiciones_ResultadoOk()
        {
            Type tipoClase = typeof(Objeto);

            Type tipoPropiedadString = typeof(string);
            Type tipoPropiedadInt = typeof(int?);

            string[] nombresPropiedadesEsperadasString = { "Texto" };
            string[] nombresPropiedadesEsperadasInt = { "Entero" };

            var resString = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DescriptionAttribute>(tipoClase, tipoPropiedadString,
                condicionAtributo: (a) => a.Description == Objeto.TEXTO_DESCRIPCION);
            var resInt = ReflectionUtilidades.ObtenerPropiedadesDeTipoConAtributo<System.ComponentModel.DataAnnotations.RangeAttribute>(tipoClase, tipoPropiedadInt,
                condicionAtributo: (a) => Object.Equals(a.Minimum, Objeto.ENTERO_RANGO_MINIMO));

            var nombresPropiedadesEncontradasString = resString.Select(p => p.Name).ToArray();
            var nombresPropiedadesEncontradasInt = resInt.Select(p => p.Name).ToArray();

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasString,
                actual: nombresPropiedadesEncontradasString);

            CollectionAssert.AreEquivalent(
                expected: nombresPropiedadesEsperadasInt,
                actual: nombresPropiedadesEncontradasInt);
        }

        #endregion
    }
}
