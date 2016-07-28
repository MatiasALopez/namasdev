using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.Core.UnitTests._TestUtils
{
    public class ObjetoConEnum
    {
        [EnumValido]
        public Enumeracion Enumeracion { get; set; }
    }
}
