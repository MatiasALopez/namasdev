using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Core.UnitTests._TestUtils
{
    public class Objeto : IValidatableObject
    {
        public const string TEXTO_DESCRIPCION = "Descripción del texto";
        public const string TEXTO_CATEGORIA = "Propiedades";

        public const int ENTERO_RANGO_MINIMO = 1;

        [Description(TEXTO_DESCRIPCION),
        Category(TEXTO_CATEGORIA)]
        public string CampoTexto;

        public Objeto()
        {
        }

        public Objeto(string texto = null, int? entero = null, Guid? guid = null, DateTime? fechaHora = null)
        {
            this.Texto = texto;
            this.Entero = entero;
            this.Guid = guid;
            this.FechaHora = fechaHora;
        }

        [Description(TEXTO_DESCRIPCION),
        Category(TEXTO_CATEGORIA)]
        public string Texto { get; set; }

        [Required,
        Range(ENTERO_RANGO_MINIMO, 100)]
        public int? Entero { get; set; }
        
        public Guid? Guid { get; set; }
        
        [Required]
        public DateTime? FechaHora { get; set; }

        public override bool Equals(object obj)
        {
            Objeto objeto = obj as Objeto;
            return objeto != null 
                && String.Equals(this.Texto, objeto.Texto) 
                && int.Equals(this.Entero, objeto.Entero)
                && System.Guid.Equals(this.Guid, objeto.Guid)
                && DateTime.Equals(this.FechaHora, objeto.FechaHora);
        }

        public override int GetHashCode()
        {
            return (this.Texto ?? String.Empty).GetHashCode() 
                ^ (this.Entero ?? 0).GetHashCode() 
                ^ (this.Guid ?? System.Guid.Empty).GetHashCode() 
                ^ (this.FechaHora ?? default(DateTime)).GetHashCode();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(Texto) && !Guid.HasValue)
            {
                yield return new ValidationResult("Debe especificar un valor para al menos una de las Propiedades: Texto o Guid.");
            }
        }
    }
}
