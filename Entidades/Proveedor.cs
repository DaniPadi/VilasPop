using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Proveedor
    {
        public Proveedor()
        {
        }

        public Proveedor(string id, string nombre, string telefono, string correo)
        {
            this.id = id;
            this.nombre = nombre;
            this.telefono = telefono;
            this.correo = correo;
        }

        public string id { set; get; }
        public string nombre { set; get; }
        public string telefono { set; get; }
        public  string correo { set; get; } 

    }
}
