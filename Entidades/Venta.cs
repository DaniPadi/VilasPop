using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Venta
    {
        public Venta(string idVenta, string dineroVenta, string idFactura, int cantidad)
        {
            this.idVenta = idVenta;
            this.dineroVenta = dineroVenta;
            this.idFactura = idFactura;
            this.cantidad = cantidad;
        }

        String idVenta { get; set; }
        String dineroVenta { get; set; }
        String idFactura { get; set; }
        int cantidad { get; set; }

    }
}
