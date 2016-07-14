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

            if (!String.IsNullOrWhiteSpace(parametros.ExpresionOrdenamiento))
            {
                bool esOrdenDescendente = parametros.ExpresionOrdenamiento.EndsWith(" DESC");
                string nombrePropiedad = parametros.ExpresionOrdenamiento.TrimEnd(" DESC".ToCharArray());

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

            if (parametros.CantMaximaRegistrosPorPagina.HasValue && parametros.DesdeIndice.HasValue)
            {
                parametros.CantTotalRegistros = query.Count();
                query = query
                    .Skip(parametros.DesdeIndice.Value)
                    .Take(parametros.CantMaximaRegistrosPorPagina.Value);
            }

            return query;
        }

    }
}
