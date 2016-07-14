using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Tipos
{
    public class StringUtilidades
    {
        private const string SEPARADOR_DEFAULT = ", ";

        public static string CrearTextoDesdeDiccionario<TKey, TValue>(IDictionary<TKey, TValue> diccionario,
            string separadorClaveValor = ": ",
            string separadorItemLista = SEPARADOR_DEFAULT)
        {
            Validador.ValidarRequerido(diccionario, "diccionario");

            return CrearTextoDesdeLista(diccionario.Select(it => String.Format("{1}{0}{2}", separadorClaveValor, it.Key, it.Value)),
                separador: separadorItemLista);
        }

        public static string CrearTextoDesdeLista<T>(IEnumerable<T> lista,
            string separador = SEPARADOR_DEFAULT,
            bool eliminarVacios = false)
        {
            Validador.ValidarRequerido(lista, "lista");

            if (eliminarVacios)
            {
                lista = lista.Where(it => !String.IsNullOrWhiteSpace(Convert.ToString(it)));
            }

            return String.Join(separador, lista);
        }
    }
}
