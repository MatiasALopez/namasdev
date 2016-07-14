using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namasdev.Validaciones;

namespace namasdev.Templates
{
    public class Plantilla
    {
        public Plantilla(string nombre, string contenido)
        {
            Validador.ValidarRequerido(nombre, "nombre");
            Validador.ValidarRequerido(contenido, "contenido");

            this.Nombre = nombre;
            this.Contenido = contenido;
        }

        public string Nombre { get; set; }
        public string Contenido { get; set; }
    }
}
