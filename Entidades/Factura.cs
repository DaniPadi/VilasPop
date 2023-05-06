using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Factura
    {
        public Factura(string id_factura, DateTime fecha, float precioTotal, string cliente)
        {
            this.id_factura = id_factura;
            this.fecha = fecha;
            this.precioTotal = precioTotal;
            this.cliente = cliente;
        }

        public string id_factura { get; set; }
        public DateTime fecha { get; set; }
        public float precioTotal { get; set; }

        public string cliente { get; set; }
    }
}
