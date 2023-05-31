using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {


        public String cedula { get; set; }
        public String nombre { get; set; }
        public String apellido { get; set; }
        public String telefono { get; set; }
        public String correo { get; set; }

        public Cliente(string cedula, string nombre,string apellido, string telefono, string correo)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.correo = correo;
        }

        public Cliente()
        {
        }
    }
}
