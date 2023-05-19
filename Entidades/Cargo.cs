using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Cargo
    {
        public Cargo(string id_cargo, string cargo, int rango)
        {
            this.id_cargo = id_cargo;
            this.cargo = cargo;
            this.rango = rango;
        }

        string id_cargo { get; set; }
        string cargo { get; set; }
        int rango { get; set; }
    }
}
