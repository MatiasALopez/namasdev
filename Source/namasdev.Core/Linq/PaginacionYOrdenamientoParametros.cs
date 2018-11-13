using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Linq
{
    public class PaginacionYOrdenamientoParametros
    {
        private int _pagina;
        private int _cantMaximaRegistrosPorPagina;

        public PaginacionYOrdenamientoParametros()
        {
            _pagina = _cantMaximaRegistrosPorPagina = 1;
        }

        public int Pagina
        {
            get { return _pagina; }
            set { _pagina = Math.Max(value, 1); }
        }

        public int CantMaximaRegistrosPorPagina
        {
            get { return _cantMaximaRegistrosPorPagina; }
            set { _cantMaximaRegistrosPorPagina = Math.Max(value, 1); }
        }

        public int CantTotalRegistros { get; set; }

        public string ExpresionOrdenamiento { get; set; }
    }
}
