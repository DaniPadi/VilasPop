using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class FacturasInfo
    {
        public FacturasInfo(int ventas, double dinero)
        {
            this.ventas = ventas;
            this.dinero = dinero;
        }

        public int ventas { get; set; }
        public double dinero { get; set; }

    }
}
