using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Factura
    {
        public Factura(string idFactura, DateTime fecha, int estado, double costo)
        {
            this.idFactura = idFactura;
            Fecha = fecha;
            Estado = estado;
            this.costo = costo;
        }

        String idFactura { get; set; }
        DateTime Fecha { get; set; }
        int Estado { get; set; }
        double costo { get; set; }


    }
}
