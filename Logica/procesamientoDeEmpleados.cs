using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Logica
{
    public class procesamientoDeEmpleados
    {
        static List<Empleado> listaEmpleados = new List<Empleado>();
        public Empleado VerificarUsuario(string usuario,string contrasena)
        {
            traerEmpleados();
          
            Empleado empleado = listaEmpleados.FirstOrDefault(e => e.cedula == usuario);
            Console.WriteLine("Cédula: " + empleado.cedula + " contraseña: " + empleado.contrasena);
        
            if (empleado != null)
            {
                if (empleado.contrasena.Equals(contrasena)) {
                    return empleado; ;
                
                }
                return null;
            }
           
            else
            {
                return null;
            }
        }

        public void traerEmpleados() 
        {
            listaEmpleados = DatosEmpleado.ObtenerEmpleadosDesdeArchivo();

            foreach (Empleado empleado in listaEmpleados)
            {
                Console.WriteLine("Cédula: " + empleado.cedula);
                Console.WriteLine("Nombre: " + empleado.nombre);
                Console.WriteLine("Teléfono: " + empleado.telefono);
                Console.WriteLine("Cargo: " + empleado.cargo);
                Console.WriteLine("Contraseña: " + empleado.contrasena);
                Console.WriteLine();
            }


        }
    }
}
