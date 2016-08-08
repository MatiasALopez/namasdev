using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using namasdev.Tipos;
using namasdev.Validaciones;

namespace namasdev.Web
{
    public class UrlBuilder
    {
        private const string REGEX_ESPACIOS_PATTERN = @"\s"; 
        private const string REGEX_MULTIPLES_ESPACIOS_Y_GUIONES_PATTERN = @"[\s-]{2,}";

        public static ushort TamañoMaximoUrlSlug = 95;

        public static string GenerarUrlSlug(string valor)
        {
            Validador.ValidarRequerido(valor, "url");

            valor = valor
                .ToLower()
                .ReemplazarAcentosYCaracteresEspecialesPorEquivalentes();

            // convertir multiples espacios/guiones en un espacio
            valor = Regex.Replace(valor, REGEX_MULTIPLES_ESPACIOS_Y_GUIONES_PATTERN, " ");
            
            valor = valor.Truncar(TamañoMaximoUrlSlug).Trim();

            // convierto espacios en guiones
            valor = Regex.Replace(valor, REGEX_ESPACIOS_PATTERN, "-");

            return valor;
        }
    }
}
