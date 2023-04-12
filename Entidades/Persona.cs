using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Persona
    {


        public String cedula { get; set; }
        public String nombre { get; set; }
        public String telefono { get; set; }
        public Persona(string cedula, string nombre, string telefono)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.telefono = telefono;
        }

    }
}
