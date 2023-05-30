using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Receta
    {
        public Receta()
        {
        }

        public Receta(string id, string receta, string productoID, string descripcion)
        {
            this.id = id;
            this.receta = receta;
            this.productoID = productoID;
            this.descripcion = descripcion;
        }

        public string id { get; set; }
        public string receta { get; set; }
        public string productoID { get; set; }

        public string descripcion { get; set; }


    }
}
