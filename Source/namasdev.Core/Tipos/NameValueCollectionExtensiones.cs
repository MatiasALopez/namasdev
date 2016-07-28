using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Tipos
{
    public static class NameValueCollectionExtensiones
    {
        /// <summary>
        /// Agregar los nuevos valores si las claves no existen. En caso de existir, reemplaza los valores actuales con los nuevos.
        /// </summary>
        /// <param name="coleccion"></param>
        /// <param name="nuevosValores"></param>
        public static void AgregarOReemplazar(this NameValueCollection coleccion, NameValueCollection nuevosValores)
        {
            Validador.ValidarRequerido(coleccion, "coleccion");

            if (nuevosValores == null)
            {
                return;
            }

            int count = nuevosValores.Count;
            for (int i = 0; i < count; i++)
            {
                coleccion.Set(nuevosValores.GetKey(i), nuevosValores.Get(i));
            }
        }
    }
}
