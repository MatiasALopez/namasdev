using namasdev.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Tipos
{
    public static class EnumExtensiones
    {
        public static string Descripcion(this Enum valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }

            DescriptionAttribute atributo = null;
            if (EsValido(valor))
            {
                atributo = ReflectionUtilidades.ObtenerAtributoDeCampo<DescriptionAttribute>(valor.GetType(), valor.ToString());
            }

            return atributo != null
                ? atributo.Description
                : valor.ToString();
        }

        public static bool EsValido(this Enum valor)
        {
            //  La "ausencia de valor" en un Enum es considerado válido.
            if (valor == null)
            {
                return true;
            }

            //  Para verificar que un valor de un Enum es válido, intentamos hacer el cast del valor a un número, 
            //  en caso de poder hacerlo asumimos que el valor NO está definido en el Enum.
            long val;
            return !long.TryParse(valor.ToString(), out val);
        }
    }
}
