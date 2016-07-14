using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace namasdev.Tipos
{
    public static class StringUtilidades
    {
        private const string SEPARADOR_DEFAULT = ", ";
        private const string REGEX_CARACTERES_NO_ALFANUMERICOS_PATTERN = "[^a-zA-Z0-9 ]";

        public static string CrearTextoDesdeDiccionario<TKey, TValue>(this IDictionary<TKey, TValue> diccionario,
            string separadorClaveValor = ": ",
            string separadorItemLista = SEPARADOR_DEFAULT)
        {
            Validador.ValidarRequerido(diccionario, "diccionario");

            return CrearTextoDesdeLista(diccionario.Select(it => String.Format("{1}{0}{2}", separadorClaveValor, it.Key, it.Value)),
                separador: separadorItemLista);
        }

        public static string CrearTextoDesdeLista<T>(this IEnumerable<T> lista,
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

        public static string Capitalizar(this string valor,
            CultureInfo cultura = null)
        {
            return (cultura ?? CultureInfo.CurrentCulture).TextInfo.ToTitleCase(valor);
        }

        public static string ReemplazarAcentosYCaracteresEspecialesPorEquivalentes(this string valor)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return String.Empty;
            }

            return new String
                (
                    valor.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray()
                )
                .Normalize(NormalizationForm.FormC);
        }

        public static string QuitarCaracteresNoAlfanumericos(this string valor)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return String.Empty;
            }

            return Regex.Replace(valor, REGEX_CARACTERES_NO_ALFANUMERICOS_PATTERN, String.Empty);
        }

        public static string Truncar(this string valor, ushort tamaño,
            string sufijo = null)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return String.Empty;
            }

            return tamaño >= valor.Length
                ? valor
                : valor.Substring(0, tamaño) + (sufijo ?? String.Empty);
        }
    }
}
