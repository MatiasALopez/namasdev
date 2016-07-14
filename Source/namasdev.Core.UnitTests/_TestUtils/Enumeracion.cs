using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Core.UnitTests._TestUtils
{
    public enum Enumeracion
    {
        [Description("Opción 1")]
        Opcion1 = 1,
        
        [Description("Opción 2")]
        Opcion2 = 2,

        [Description("Opción 3")]
        Opcion3 = 3
    }
}
