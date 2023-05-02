using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Producto
    {
        public Producto(string idProducto, string nombreProducto, int stock, double precio, bool estado)
        {
            this.idProducto = idProducto;
            this.nombreProducto = nombreProducto;
            this.stock = stock;
            this.precio = precio;
            this.estado = estado;
        }

        String idProducto { get; set; }
        String nombreProducto { get; set; }
        int stock { get; set; }
        double precio { get; set; }
        bool estado { get; set; }

    }
}
