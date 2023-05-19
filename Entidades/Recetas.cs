using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Recetas
    {
        public Recetas(string id, string materiaPrima, int unidades, float mililitros, float gramos)
        {
            this.id = id;
            this.materiaPrima = materiaPrima;
            this.unidades = unidades;
            this.mililitros = mililitros;
            this.gramos = gramos;
        }

        public string id { get; set; }
        public string materiaPrima { get; set; }
        public int unidades { get; set; }
        public float mililitros { get; set; }
        public float gramos { get; set; }
    }
}
