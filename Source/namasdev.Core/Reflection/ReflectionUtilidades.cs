using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Reflection
{
    public class ReflectionUtilidades
    {
        public static object ObtenerValorDefault(Type tipo)
        {
            Validador.ValidarRequerido(tipo, "tipo");

            if (tipo.IsValueType)
            {
                return Activator.CreateInstance(tipo);
            }

            return null;
        }

        public static bool LasPropiedadesDeLosObjetosSonIguales<T>(T obj1, T obj2) 
            where T : class
        {
            Validador.ValidarRequerido(obj1, "obj1");
            Validador.ValidarRequerido(obj2, "obj2");

            if (obj1 == obj2)
            {
                return true;
            }

            Type tipo = typeof(T);
            //
            foreach (var pi in ObtenerPropiedadesDeTipo(tipo))
            {
                object obj1Value = tipo.GetProperty(pi.Name).GetValue(obj1, null);
                object obj2Value = tipo.GetProperty(pi.Name).GetValue(obj2, null);

                if (obj1Value != obj2Value && (obj1Value == null || !obj1Value.Equals(obj2Value)))
                {
                    return false;
                }
            }

            return true;
        }

        private static PropertyInfo[] ObtenerPropiedadesDeTipo(Type tipo)
        {
            return tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public static T CrearInstancia<T>(params object[] parametrosConstructor)
            where T : class
        {
            return (T)Activator.CreateInstance(typeof(T), parametrosConstructor);
        }

        public static bool TodasLasPropiedadesDelObjetoTienenValorNull<T>(T objeto)
            where T : class
        {
            Validador.ValidarRequerido(objeto, "objeto");

            return !ObtenerPropiedadesDeTipo(typeof(T))
                .Select(p => p.GetValue(objeto))
                .Any(v => v != null && !String.IsNullOrWhiteSpace(Convert.ToString(v)));
        }

        public static bool TodasLasPropiedadesDelObjetoTienenValorDefault<T>(T objeto)
            where T : class
        {
            Validador.ValidarRequerido(objeto, "objeto");

            return !ObtenerPropiedadesDeTipo(typeof(T))
                .Any(p => !object.Equals(p.GetValue(objeto), ObtenerValorDefault(p.PropertyType)));
        }

        public static TAtributo ObtenerAtributoDeCampo<TAtributo>(Type tipo, string nombreCampo)
            where TAtributo : Attribute
        {
            Validador.ValidarRequerido(tipo, "tipo");
            Validador.ValidarRequerido(nombreCampo, "nombreCampo");

            var campo = tipo.GetField(nombreCampo);
            if (campo == null)
            {
                throw new MissingMemberException(tipo.FullName, nombreCampo);
            }

            return ObtenerAtributoDeMiembro<TAtributo>(campo);
        }

        public static TAtributo ObtenerAtributoDePropiedad<TAtributo>(Type tipo, string nombrePropiedad)
           where TAtributo : Attribute
        {
            Validador.ValidarRequerido(tipo, "tipo");
            Validador.ValidarRequerido(nombrePropiedad, "nombrePropiedad");

            var propiedad = tipo.GetProperty(nombrePropiedad);
            if (propiedad == null)
            {
                throw new MissingMemberException(tipo.FullName, nombrePropiedad);
            }

            return ObtenerAtributoDeMiembro<TAtributo>(propiedad);
        }

        private static TAtributo ObtenerAtributoDeMiembro<TAtributo>(MemberInfo miembro)
          where TAtributo : Attribute
        {
            Validador.ValidarRequerido(miembro, "miembro");

            var atributo = miembro.GetCustomAttributes(typeof(TAtributo), false);
            return atributo != null && atributo.Length > 0
                ? (TAtributo)atributo.FirstOrDefault()
                : null;
        }
    }
}
