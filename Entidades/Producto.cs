using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public Producto()
        {
        }

        public Producto(string idProducto, string nombreProducto, float precio)
        {
            this.idProducto = idProducto;
            this.nombreProducto = nombreProducto;
          
            this.precio = precio;
          
        }

        public String idProducto { get; set; }
        public String nombreProducto { get; set; }
       
        public float precio { get; set; }


    }
}
