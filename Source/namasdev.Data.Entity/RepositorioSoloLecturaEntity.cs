using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using namasdev.Linq;

namespace namasdev.Data.Entity
{
    public abstract class RepositorioSoloLecturaEntity<TContext, TEntidad, TId> : IRepositorioSoloLectura<TEntidad, TId>
        where TContext : DbContext, new()
        where TEntidad : class
    {
        public TEntidad Obtener(TId id)
        {
            using (var ctx = CrearContext())
            {
                return Set(ctx).Find(id) as TEntidad;
            }
        }

        public IEnumerable<TEntidad> ObtenerTodos(PaginacionYOrdenamientoParametros parametros = null)
        {
            using (var ctx = CrearContext())
            {
                var query = Set(ctx) as IQueryable<TEntidad>;

                if (parametros != null)
                {
                    query = query.AplicarPaginacionYOrdenamiento(parametros);
                }

                return query.ToList();
            }
        }

        public bool Existe(Expression<Func<TEntidad, bool>> condicion)
        {
            using (var ctx = CrearContext())
            {
                return Set(ctx).Count(condicion) > 0;
            }
        }

        public bool ExistePorId(TId id)
        {
            using (var ctx = CrearContext())
            {
                return Set(ctx).Count(CrearCondicionDeBusquedaPorId(id)) > 0;
            }
        }

        protected abstract Expression<Func<TEntidad, bool>> CrearCondicionDeBusquedaPorId(TId id);

        protected DbContext CrearContext()
        {
            return new TContext();
        }

        protected TContext CrearContext<TContext>()
            where TContext : DbContext, new()
        {
            return new TContext();
        }

        protected DbSet<TEntidad> Set(DbContext ctx)
        {
            return ctx.Set<TEntidad>();
        }
    }
}
