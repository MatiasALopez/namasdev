using namasdev.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Net.UnitTests._TestUtils
{
    public class RepositorioPlantillasEnMemoria : IRepositorioPlantillas
    {
        private IEnumerable<Plantilla> _plantillas;

        public RepositorioPlantillasEnMemoria(IEnumerable<Plantilla> plantillas)
        {
            _plantillas = plantillas ?? new List<Plantilla>();
        }

        public Plantilla Obtener(string nombre)
        {
            return _plantillas
                .SingleOrDefault(p => p.Nombre == nombre);
        }
    }
}
