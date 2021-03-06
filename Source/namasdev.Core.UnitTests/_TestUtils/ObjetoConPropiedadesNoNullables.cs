﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace namasdev.Core.UnitTests._TestUtils
{
    public class ObjetoConPropiedadesNoNullables
    {
        public ObjetoConPropiedadesNoNullables()
        {
        }

        public ObjetoConPropiedadesNoNullables(string texto, int entero, Guid guid, DateTime fechaHora)
        {
            this.Texto = texto;
            this.Entero = entero;
            this.Guid = guid;
            this.FechaHora = fechaHora;
        }

        public string Texto { get; set; }
        public int Entero { get; set; }
        public Guid Guid { get; set; }
        public DateTime FechaHora { get; set; }

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
            return this.Texto.GetHashCode() ^ this.Entero.GetHashCode() ^ this.Guid.GetHashCode() ^ this.FechaHora.GetHashCode();
        }
    }
}
