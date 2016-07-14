using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Data
{
    public interface IEntidadConAuditoria
    {
        string CreadoPor { get; set; }
        DateTime CreadoFecha { get; set; }
        string UltimaModificacionPor { get; set; }
        DateTime UltimaModificacionFecha { get; set; }
        string BorradoPor { get; set; }
        DateTime? BorradoFecha { get; set; }
    }
}
