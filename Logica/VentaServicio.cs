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
        string connection;
        public VentaServicio(string stringConnection)
        {
            this.ventaRepositorio = new VentaRepositorio(stringConnection);
            connection= stringConnection;
        }

        public int Insert(List<Vendidos> ventas) 
        {
            int filas = 0;
            foreach (Vendidos venta in ventas) 
            {
                filas += ventaRepositorio.Insert(venta);
            }
            return filas;
        }

        public List<Vendidos> obtenerVentasConFactura(string factura) 
        {
            return ventaRepositorio.obtenerVentasConFactura(factura);
        }  

        public List<VendidoDTO> convertriDTO(List<Vendidos> vendidos) 
        {
            List<VendidoDTO> vendidosDTO= new List<VendidoDTO>();
            ProductoServicio servicioProducto = new ProductoServicio(connection);
            foreach (Vendidos vendido in vendidos) 
            {
            VendidoDTO vendidoDTO = new VendidoDTO();
                vendidoDTO.FACTURA = vendido.id_factura;
                vendidoDTO.PRODUCTO = servicioProducto.obtenerProductoPorID(vendido.id_producto).nombreProducto;
                vendidoDTO.VALOR = vendido.valor;
                vendidoDTO.CANTIDAD = vendido.cantidad;
                vendidosDTO.Add(vendidoDTO);
            }


            return vendidosDTO;

        }
    }
}
