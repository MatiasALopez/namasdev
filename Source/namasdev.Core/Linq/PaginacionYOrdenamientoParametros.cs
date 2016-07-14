using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Linq
{
    public class PaginacionYOrdenamientoParametros
    {
        public int? DesdeIndice { get; set; }
        public int? CantMaximaRegistrosPorPagina { get; set; }
        public int CantTotalRegistros { get; set; }
        public string ExpresionOrdenamiento { get; set; }
    }
}
