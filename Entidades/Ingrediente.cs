using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Ingrediente
    {
        public Ingrediente()
        {
        }

        public Ingrediente(string idReceta, string materiaPrima, int unidades, float mililitros, float gramos)
        {
            this.idReceta = idReceta;
            this.idmateriaPrima = materiaPrima;
            this.unidades = unidades;
            this.mililitros = mililitros;
            this.gramos = gramos;
        }

        public string idReceta { get; set; }
        public string idmateriaPrima { get; set; }
        public int unidades { get; set; }
        public float mililitros { get; set; }
        public float gramos { get; set; }
    }
}
