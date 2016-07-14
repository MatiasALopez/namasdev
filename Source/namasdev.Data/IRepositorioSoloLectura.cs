using namasdev.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using namasdev.Linq;

namespace namasdev.Data
{
    public interface IRepositorioSoloLectura<TEntidad, TId>
        where TEntidad : class
    {
        TEntidad Obtener(TId id);
        IEnumerable<TEntidad> ObtenerTodos(PaginacionYOrdenamientoParametros parametros = null);
        bool Existe(Expression<Func<TEntidad, bool>> condicion);
        bool ExistePorId(TId id);
    }
}
