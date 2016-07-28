using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using namasdev.Tipos;

namespace namasdev.Core.UnitTests.Tipos
{
    [TestClass]
    public class DateTimeUtilidadesTest
    {
        #region CrearDesdeFechaYHora

        [TestMethod]
        public void CrearDesdeFechaYHora_ValoresDefault_ResultadoOk()
        {
            DateTime fecha = default(DateTime);
            TimeSpan hora = default(TimeSpan);

            DateTime fechaHora = DateTimeUtilidades.CrearDesdeFechaYHora(fecha, hora);

            Assert.AreEqual(
                expected: new DateTime(fecha.Year, fecha.Month, fecha.Day, hora.Hours, hora.Minutes, hora.Seconds, hora.Milliseconds),
                actual: fechaHora);
        }

        [TestMethod]
        public void CrearDesdeFechaYHora_ValoresOk_ResultadoOk()
        {
            DateTime fecha = new DateTime(2016, 6, 15);
            TimeSpan hora = new TimeSpan(17, 31, 45, 269);

            DateTime fechaHora = DateTimeUtilidades.CrearDesdeFechaYHora(fecha, hora);

            Assert.AreEqual(
                expected: new DateTime(fecha.Year, fecha.Month, fecha.Day, hora.Hours, hora.Minutes, hora.Seconds, hora.Milliseconds),
                actual: fechaHora);
        }

        #endregion

        #region CalcularTiempoTranscurrido

        [TestMethod,
        ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CalcularTiempoTranscurrido_RangoInvalido_ArrojaExcepcion()
        {
            DateTime inicio = new DateTime(2016, 10, 5, 10, 30, 0),
                fin         = new DateTime(2016, 10, 5, 10, 0, 0);

            DateTimeUtilidades.CalcularTiempoTranscurrido(inicio, fin);
        }

        [TestMethod]
        public void CalcularTiempoTranscurrido_ValoresOk_ResultadoOk()
        {
            TimeSpan tiempo = new TimeSpan(2, 15, 10);
            DateTime inicio = new DateTime(2016, 10, 5, 10, 0, 0),
                fin = inicio.Add(tiempo);

            TimeSpan tiempoTranscurrido = DateTimeUtilidades.CalcularTiempoTranscurrido(inicio, fin);

            Assert.AreEqual(
                expected: tiempo,
                actual: tiempoTranscurrido);
        }

        [TestMethod]
        public void CalcularTiempoTranscurrido_MismosValores_ResultadoOk()
        {
            DateTime inicio = new DateTime(2016, 10, 5, 10, 0, 0),
                fin = inicio;

            TimeSpan tiempoTranscurrido = DateTimeUtilidades.CalcularTiempoTranscurrido(inicio, fin);

            Assert.AreEqual(
                expected: new TimeSpan(),
                actual: tiempoTranscurrido);
        }

        #endregion
    }
}
