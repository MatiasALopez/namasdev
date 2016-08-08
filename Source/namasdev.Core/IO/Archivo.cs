using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.IO
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

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
