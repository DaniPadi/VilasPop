using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ProveedorServicio
    {
        ProveedorRepositorio proveedorPrepositorio;

        public ProveedorServicio(string connectionstring)
        {
            proveedorPrepositorio = new ProveedorRepositorio(connectionstring);
        }

        public string Insert(Proveedor proveedor) 
        {
            return proveedorPrepositorio.Insert(proveedor);
        }

        public List<Proveedor> obtenerProveedores() 
        {
        return proveedorPrepositorio.obtenerProveedores();
        }

        public Proveedor ObtenerProveedorPorCodigo( string nombre)
        {
            List<Proveedor> proveedores = obtenerProveedores();
            Proveedor proveedorEncontrado = null;

            foreach (Proveedor proveedor in proveedores)
            {
                if (proveedor.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    proveedorEncontrado = proveedor;
                    break;
                }
            }

            return proveedorEncontrado;
        }
    }
}
