using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Linq
{
    public class PaginacionYOrdenamientoParametros
    {
        private int _desdeIndice;
        private int _cantMaximaRegistrosPorPagina;

        public PaginacionYOrdenamientoParametros()
        {
            _desdeIndice = 0;
            _cantMaximaRegistrosPorPagina = 1;
        }

        public PaginacionYOrdenamientoParametros(int pagina, int cantMaximaRegistrosPorPagina)
        {
            CantMaximaRegistrosPorPagina = cantMaximaRegistrosPorPagina;
            DesdeIndice = (pagina - 1) * CantMaximaRegistrosPorPagina;
        }

        public int DesdeIndice
        {
            get { return _desdeIndice; }
            set { _desdeIndice = Math.Max(value, 0); }
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
