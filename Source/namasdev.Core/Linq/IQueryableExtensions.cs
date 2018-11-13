using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> AplicarPaginacionYOrdenamiento<T>(this IQueryable<T> query, PaginacionYOrdenamientoParametros parametros)
        {
            Validador.ValidarRequerido(query, "query");
            Validador.ValidarRequerido(parametros, "parametros");

            const string ORDENAMIENTO_SUFIJO_DESC = " desc";
            if (!String.IsNullOrWhiteSpace(parametros.ExpresionOrdenamiento))
            {
                bool esOrdenDescendente = parametros.ExpresionOrdenamiento.EndsWith(ORDENAMIENTO_SUFIJO_DESC, StringComparison.CurrentCultureIgnoreCase);
                string nombrePropiedad =
                    esOrdenDescendente
                    ? parametros.ExpresionOrdenamiento.Remove(parametros.ExpresionOrdenamiento.Length - ORDENAMIENTO_SUFIJO_DESC.Length)
                    : parametros.ExpresionOrdenamiento;

                var tipo = typeof(T);
                var prop = tipo.GetProperty(nombrePropiedad);
                if (prop == null)
                {
                    throw new ArgumentException(String.Format("La propiedad {0} no existe.", nombrePropiedad), "parametros.SortByExpression");
                }

                var param = Expression.Parameter(tipo, "p");
                var orderByExpresion = Expression.Lambda(Expression.MakeMemberAccess(param, prop), param);
                MethodCallExpression resultExp =
                    Expression.Call(
                        typeof(Queryable),
                        esOrdenDescendente ? "OrderByDescending" : "OrderBy",
                        new Type[] { tipo, prop.PropertyType },
                        query.Expression,
                        Expression.Quote(orderByExpresion));

                query = query.Provider.CreateQuery<T>(resultExp);
            }

            if (parametros.CantMaximaRegistrosPorPagina > 0)
            {
                parametros.CantTotalRegistros = query.Count();

                query = query
                    .Skip(parametros.DesdeIndice)
                    .Take(parametros.CantMaximaRegistrosPorPagina);
            }

            return query;
        }

    }
}
