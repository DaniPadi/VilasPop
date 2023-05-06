using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Datos
{
    public class DatosEmpleado
    {
        public static List<Empleado> ObtenerEmpleadosDesdeArchivo()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
               
                string[] lineas = File.ReadAllLines("empleados.txt");

               
                foreach (string linea in lineas)
                {
                    string[] datosEmpleado = linea.Split(',');
                    Empleado empleado = new Empleado(datosEmpleado[0], datosEmpleado[1], datosEmpleado[2], datosEmpleado[3], datosEmpleado[4]);
                    listaEmpleados.Add(empleado);
                }
            }
            catch (Exception ex)
            {
           
                Console.WriteLine("Error al leer el archivo de empleados: " + ex.Message);
            }

            return listaEmpleados;
        }
    }
}
