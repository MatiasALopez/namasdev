using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Tipos
{
    public static class DateTimeUtilidades
    {
        public static DateTime CrearDesdeFechaHora(DateTime fecha, TimeSpan hora)
        {
            return new DateTime(fecha.Year, fecha.Month, fecha.Day, hora.Hours, hora.Minutes, hora.Seconds);
        }

        public static TimeSpan CalcularTiempoTranscurrido(DateTime inicio, DateTime fin)
        {
            return fin.Subtract(inicio);
        }
    }
}
