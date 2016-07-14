using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Data.Entity
{
    public abstract class RepositorioEntity<TContext, TEntidad, TId> : RepositorioSoloLecturaEntity<TContext, TEntidad, TId>, IRepositorio<TEntidad, TId>
        where TContext : DbContext, new()
        where TEntidad : class
    {
        public void Agregar(IEnumerable<TEntidad> entidades)
        {
            using (var ctx = CrearContext())
            {
                foreach (var entidad in entidades)
                {
                    EstablecerEstadoDeEntidad(ctx, entidad, EntityState.Added);
                }

                GuardarCambios(ctx);
            }
        }

        public void Agregar(TEntidad entidad)
        {
            using (var ctx = CrearContext())
            {
                EstablecerEstadoDeEntidadYGuardarCambios(ctx, entidad, EntityState.Added);
            }
        }

        public void Actualizar(IEnumerable<TEntidad> entidades)
        {
            using (var ctx = CrearContext())
            {
                foreach (var entidad in entidades)
                {
                    EstablecerEstadoDeEntidad(ctx, entidad, EntityState.Modified);
                }

                GuardarCambios(ctx);
            }
        }

        public void Actualizar(TEntidad entidad)
        {
            using (var ctx = CrearContext())
            {
                EstablecerEstadoDeEntidadYGuardarCambios(ctx, entidad, EntityState.Modified);
            }
        }

        public void Eliminar(IEnumerable<TEntidad> entidades)
        {
            using (var ctx = CrearContext())
            {
                foreach (var entidad in entidades)
                {
                    EstablecerEstadoDeEntidad(ctx, entidad, EntityState.Deleted);
                }

                GuardarCambios(ctx);
            }
        }

        public void Eliminar(TEntidad entidad)
        {
            using (var ctx = CrearContext())
            {
                EstablecerEstadoDeEntidadYGuardarCambios(ctx, entidad, EntityState.Deleted);
            }
        }

        public void EliminarPorId(IEnumerable<TId> ids)
        {
            var entidades = new List<TEntidad>();
            foreach(var id in ids)
            {
                entidades.Add(CrearEntidadSoloConId(id));
            }

            Eliminar(entidades);
        }

        public void EliminarPorId(TId id)
        {
            var entidad = CrearEntidadSoloConId(id);
            Eliminar(entidad);
        }

        protected IQueryable<TEntidad> GenerarQueryConIncludes(DbContext ctx, IEnumerable<Expression<Func<TEntidad, object>>> includes)
        {
            var query = Set(ctx) as IQueryable<TEntidad>;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        protected void EstablecerEstadoDeEntidad(DbContext ctx, TEntidad entidad, EntityState estado)
        {
            ctx.Entry(entidad).State = estado;
        }

        private void GuardarCambios(DbContext ctx)
        {
            ctx.SaveChanges();
        }

        private void EstablecerEstadoDeEntidadYGuardarCambios(DbContext ctx, TEntidad entidad, EntityState estado)
        {
            EstablecerEstadoDeEntidad(ctx, entidad, estado);
            GuardarCambios(ctx);
        }

        public abstract TEntidad CrearEntidadSoloConId(TId id);
        protected override abstract Expression<Func<TEntidad, bool>> CrearCondicionDeBusquedaPorId(TId id);
    }
}
