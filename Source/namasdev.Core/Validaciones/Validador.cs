using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using namasdev.Excepciones;
using namasdev.Tipos;
using namasdev.Reflection;

namespace namasdev.Validaciones
{
    public class Validador
    {
        public static void ValidarRequerido(object valor, string mensaje)
        {
            ValidarRequerido<ArgumentNullException>(valor, mensaje);
        }

        public static void ValidarRequerido<TException>(object valor, string mensaje)
            where TException : Exception
        {
            if (String.IsNullOrWhiteSpace(Convert.ToString(valor)))
            {
                throw ReflectionUtilidades.CrearInstancia<TException>(mensaje);
            }
        }

        public static IEnumerable<ValidationResult> ObtenerErroresDeValidacion<T>(T objeto)
            where T : class
        {
            var res = new List<ValidationResult>();
            Validator.TryValidateObject(objeto, new ValidationContext(objeto), res, true);
            return res;
        }

        public static void Validar<TObjeto>(TObjeto objeto,
            string mensajeEncabezado = null)
            where TObjeto : class
        {
            Validar<TObjeto, ExcepcionMensajeAlUsuario>(objeto, mensajeEncabezado: mensajeEncabezado);
        }

        public static void Validar<TObjeto, TException>(TObjeto objeto, 
            string mensajeEncabezado = null)
            where TObjeto : class
            where TException : Exception
        {
            var errores = ObtenerErroresDeValidacion(objeto);
            if (errores.Any())
            {
                throw ReflectionUtilidades.CrearInstancia<TException>(GenerarMensaje(mensajeEncabezado, StringUtilidades.CrearTextoDesdeLista(errores, separador: Environment.NewLine)));
            }
        }
        
        private static string GenerarMensaje(string encabezado, string mensaje)
        {
            if (String.IsNullOrWhiteSpace(encabezado)) 
            {
                return mensaje;
            }

            return StringUtilidades.CrearTextoDesdeLista(new string[] { encabezado, mensaje }, separador: Environment.NewLine);
        }
    }
}
