using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Data
{
    public static class IEntidadConAuditoriaExtensiones
    {
        /// <summary>
        /// Establece los valores de auditoría en base al usuario y fecha/hora especificados.
        /// Esta operación solo establece los valores en el objeto, luego es necesario aplicar los cambios de manera persistente.
        /// </summary>
        /// <param name="incluirDatosAlta">Indica si se deben establecer los valores de auditoría relacionados a la creación.</param>
        public static void EstablecerDatosAuditoria(this IList<IEntidadConAuditoria> entidades, string nombreUsuario, DateTime fechaHora,
            bool incluirDatosAlta = false)
        {
            Validador.ValidarRequerido(entidades, "entidades");

            foreach (var entidad in entidades)
            {
                entidad.EstablecerDatosAuditoria(nombreUsuario, fechaHora,
                    incluirDatosAlta: incluirDatosAlta);
            }
        }

        /// <summary>
        /// Establece los valores de auditoría en base al usuario y fecha/hora especificados.
        /// Esta operación solo establece los valores en el objeto, luego es necesario aplicar los cambios de manera persistente.
        /// </summary>
        /// <param name="incluirDatosAlta">Indica si se deben establecer los valores de auditoría relacionados a la creación.</param>
        public static void EstablecerDatosAuditoria(this IEntidadConAuditoria entidad, string nombreUsuario, DateTime fechaHora,
            bool incluirDatosAlta = false)
        {
            Validador.ValidarRequerido(entidad, "entidad");

            entidad.UltimaModificacionPor = nombreUsuario;
            entidad.UltimaModificacionFecha = fechaHora;

            if (incluirDatosAlta)
            {
                entidad.CreadoPor = nombreUsuario;
                entidad.CreadoFecha = fechaHora;
            }
        }

        /// <summary>
        /// Establece los valores de auditoría de borrado en base al usuario y fecha/hora especificados.
        /// Esta operación solo establece los valores en el objeto, luego es necesario aplicar los cambios de manera persistente.
        /// </summary>
        public static void EstablecerDatosBorrado(this IList<IEntidadConAuditoria> entidades, string nombreUsuario, DateTime fechaHora)
        {
            Validador.ValidarRequerido(entidades, "entidades");

            foreach (var entidad in entidades)
            {
                entidad.EstablecerDatosBorrado(nombreUsuario, fechaHora);
            }
        }

        /// <summary>
        /// Establece los valores de auditoría de borrado en base al usuario y fecha/hora especificados.
        /// Esta operación solo establece los valores en el objeto, luego es necesario aplicar los cambios de manera persistente.
        /// </summary>
        public static void EstablecerDatosBorrado(this IEntidadConAuditoria entidad, string nombreUsuario, DateTime fechaHora)
        {
            Validador.ValidarRequerido(entidad, "entidad");

            entidad.BorradoPor = nombreUsuario;
            entidad.BorradoFecha = fechaHora;
        }
    }
}
