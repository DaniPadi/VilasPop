using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Gasto
    {
        public Gasto(string idGasto, float costo, Proveedor proveedor,  MateriaPrima materiaPrima, DateTime fecha,  float gramos, float mililitros, int unidades)
        {
            this.idGasto = idGasto;
            this.costo = costo;
            this.materiaPrima = materiaPrima;
            this.fecha = fecha;
            this.unidades = unidades;
            this.mililitros = mililitros;
            this.gramos = gramos;
            this.proveedor= proveedor;

        }

        public String idGasto { get; set; }
        public float costo { get; set; }
        public MateriaPrima materiaPrima { get; set; }
        public DateTime fecha { get; set; }
        public int unidades { get; set; }
        public float mililitros { get; set; }
        public float gramos { get; set; }
        public Proveedor proveedor { get; set; }
        
    }
}
