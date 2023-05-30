using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Logica
{
    public class VentaServicio
    {
        VentaRepositorio ventaRepositorio;

        public VentaServicio(string stringConnection)
        {
            this.ventaRepositorio = new VentaRepositorio(stringConnection);
        }

        public int Insert(List<Venta> ventas) 
        {
            int filas = 0;
            foreach (Venta venta in ventas) 
            {
                filas += ventaRepositorio.Insert(venta);
            }

            return filas;
        
        }

        public List<Venta> obtenerVentasConFactura(string factura) 
        {

            return ventaRepositorio.obtenerVentasConFactura(factura);
        }

       
    }
}
