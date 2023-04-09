using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Cargo
    {
        public Cargo(string idCargo, string nombreCargo, int rango)
        {
            this.idCargo = idCargo;
            this.nombreCargo = nombreCargo;
            this.rango = rango;
        }

        String idCargo { get; set; }
        String nombreCargo { get; set; }
        int rango { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Cargo cargo &&
                   idCargo == cargo.idCargo &&
                   nombreCargo == cargo.nombreCargo &&
                   rango == cargo.rango;
        }
    }
}
