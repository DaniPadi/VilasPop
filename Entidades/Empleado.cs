using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleado: Persona
    {

        public String cargo { get; set; }
        public String contrasena { get; set; }
        public Empleado(string cedula, string nombre, string telefono,String cargo,String contrasena) : base(cedula, nombre, telefono)
        {
            this.cargo = cargo;
            this.contrasena = contrasena;
        }


    }
}
