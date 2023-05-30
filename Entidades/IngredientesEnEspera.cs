using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class IngredientesEnEspera
    {
        public IngredientesEnEspera(MateriaPrima materiaP, string cantidad, string unidad)
        {
            this.materiaP = materiaP;
            this.cantidad = cantidad;
            this.unidad = unidad;
        }

        public MateriaPrima materiaP { get; set; }
        public string cantidad { get; set; }
        public string unidad { get; set;}
    }
}
