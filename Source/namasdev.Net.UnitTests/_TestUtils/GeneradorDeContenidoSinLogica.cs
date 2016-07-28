using namasdev.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Net.UnitTests._TestUtils
{
    public class GeneradorDeContenidoSinLogica : IGeneradorDeContenido
    {
        public string GenerarContenido<TModelo>(Plantilla plantilla, TModelo modelo = null) where TModelo : class
        {
            return plantilla.Contenido;
        }
    }
}
