using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Data
{
    public interface IRepositorio<TEntidad, TId> : IRepositorioSoloLectura<TEntidad, TId>
        where TEntidad : class
    {
        void Agregar(IEnumerable<TEntidad> entidades);
        void Agregar(TEntidad entidad);
        void Actualizar(IEnumerable<TEntidad> entidades);
        void Actualizar(TEntidad entidad);
        void Eliminar(IEnumerable<TEntidad> entidades);
        void Eliminar(TEntidad entidad);
        void EliminarPorId(IEnumerable<TId> ids);
        void EliminarPorId(TId id);
        TEntidad CrearEntidadSoloConId(TId id);
    }
}
