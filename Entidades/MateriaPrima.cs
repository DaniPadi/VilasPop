using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class MateriaPrima
    {
        public MateriaPrima(string idMateriaPrima, string nombreMateriaPrima, string cantidad, double costo, DateTime fechaCaducidad    )
        {
            this.idMateriaPrima = idMateriaPrima;
            this.nombreMateriaPrima = nombreMateriaPrima;
            this.cantidad = cantidad;
            this.costo = costo;
            this.fechaCaducidad = fechaCaducidad;
        }

        String idMateriaPrima { get; set; }
        String nombreMateriaPrima { get; set;}
        String cantidad { get; set; }
        double costo { get; set; }
        DateTime fechaCaducidad { get; set; }

    }
}
