using namasdev.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Archivos
{
    public class Archivo
    {
        public Archivo(string nombre,
            byte[] contenido = null)
        {
            Validador.ValidarRequerido(nombre, "nombre");

            Nombre = nombre;
            Contenido = contenido;
        }

        public string Nombre { get; private set; }
        public byte[] Contenido { get; set; }
    }
}
