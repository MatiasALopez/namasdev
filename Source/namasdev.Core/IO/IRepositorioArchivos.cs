using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.IO
{
    public interface IRepositorioArchivos
    {
        string Guardar(string nombreContenedor, string nombresDirectorios, Archivo archivo);
        byte[] Obtener(string nombreContenedor, string nombresDirectorios, string nombreArchivo);
        string ObtenerTexto(string nombreContenedor, string nombresDirectorios, string nombreArchivo, Encoding encoding = null);
        string Eliminar(string nombreContenedor, string nombresDirectorios, string nombreArchivo);
    }
}
