using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Templates
{
    public interface IGeneradorDeContenido
    {
        string GenerarContenido<TModelo>(Plantilla plantilla, TModelo modelo = null)
            where TModelo : class;
    }
}
