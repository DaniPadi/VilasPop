using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Persona
    {


        String cedula { get; set; }
        String nombre { get; set; }
        String telefono { get; set; }
        public Persona(string cedula, string nombre, string telefono)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.telefono = telefono;
        }

    }
}
