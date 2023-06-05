using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Vendidos
    {
        public Vendidos()
        {
        }

        public Vendidos(string id_producto, string id_factura, int cantidad, float valor)
        {
            this.id_producto = id_producto;
            this.id_factura = id_factura;
            this.cantidad = cantidad;
            this.valor = valor;
        }

        public string id_producto { get; set; }
        public string id_factura { get;set; }
        public int cantidad { get; set; }
        public float valor { get; set; }

      
    }
}
