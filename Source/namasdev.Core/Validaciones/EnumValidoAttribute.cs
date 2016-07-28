using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namasdev.Tipos;

namespace namasdev.Validaciones
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EnumValidoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //  La "ausencia de valor" en un Enum es considerado válido.
            if (value == null)
            {
                return true;
            }

            //  Si el valor NO es un Enum, no aplica la validación. Para no lanzar un error, decimos que NO es válido
            if (!(value is Enum))
            {
                return false;
            }

            return ((Enum)value).EsValido();
        }
    }
}
