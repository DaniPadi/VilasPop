using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Gasto
    {
        public Gasto(string idGasto, double costo, DateTime fecha, int cantidad, string descripcion)
        {
            this.idGasto = idGasto;
            this.costo = costo;
            this.fecha = fecha;
            this.cantidad = cantidad;
            this.descripcion = descripcion;
        }

        String idGasto { get; set; }
        double costo { get; set; }
        DateTime fecha { get; set; }
        int cantidad { get; set; }
        String descripcion { get; set; }
    }
}
