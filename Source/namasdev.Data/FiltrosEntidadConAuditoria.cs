using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Data
{
    public class FiltrosEntidadConAuditoria
    {
        public DateTime? CreadoFechaDesde { get; set; }
        public DateTime? UltimaModificacionFechaDesde { get; set; }
        public bool IncluirBorrados { get; set; }

        public IQueryable<T> Aplicar<T>(IQueryable<T> query)
            where T : class, IEntidadConAuditoria
        {
            Validador.ValidarRequerido(query, "query");

            if (CreadoFechaDesde.HasValue)
            {
                query = query
                    .Where(e => e.CreadoFecha >= CreadoFechaDesde.Value);
            }

            if (UltimaModificacionFechaDesde.HasValue)
            {
                query = query
                    .Where(e => e.UltimaModificacionFecha >= UltimaModificacionFechaDesde.Value);

                if (!CreadoFechaDesde.HasValue)
                {
                    //  NOTA: Si no especificó un filtro para la fecha de creación tenemos que evitar que las fechas
                    //  de modificacion y creación sean iguales, para que no se consideren los nuevos como modificados
                    query = query
                        .Where(e => e.UltimaModificacionFecha != e.CreadoFecha);
                }
            }

            if (!IncluirBorrados)
            {
                query = query
                    .Where(e => e.BorradoPor == null && e.BorradoFecha == null);
            }

            return query;
        }
    }
}
