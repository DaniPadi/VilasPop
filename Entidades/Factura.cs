using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Factura
    {
        public Factura()
        {
        }

        public Factura(string id_factura, DateTime fecha, float precioTotal, string cliente, string idMetodo)
        {
            this.id_factura = id_factura;
            this.fecha = fecha;
            this.precioTotal = precioTotal;
            this.IdCliente = cliente;
            IdMetodo = idMetodo;
        }

        public string id_factura { get; set; }
        public DateTime fecha { get; set; }
        public float precioTotal { get; set; }

        public string IdCliente { get; set; }

        public string IdMetodo { get; set; }
    }
}
