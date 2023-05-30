using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MateriaPrima
    {
        public MateriaPrima()
        {
        }

        public MateriaPrima(string idMateriaPrima, string nombreMateriaPrima, DateTime fechaCaducidad, int unidades, float mililitros, float gramos)
        {
            this.idMateriaPrima = idMateriaPrima;
            this.nombreMateriaPrima = nombreMateriaPrima;
            this.fechaCaducidad = fechaCaducidad;
            this.unidades = unidades;
            this.mililitros = mililitros;
            this.gramos = gramos;
        }

        public String idMateriaPrima { get; set; }
        public String nombreMateriaPrima { get; set;}
        public DateTime fechaCaducidad { get; set; }

        public int unidades { get; set; }
        public float mililitros { get; set; }
        public float gramos { get; set; }

    }
}
