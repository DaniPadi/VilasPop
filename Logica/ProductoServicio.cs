using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ProductoServicio
    {
        ProductoRepositorio productoRepositorio;
        public ProductoServicio(string connectionString)
        {
            productoRepositorio = new ProductoRepositorio(connectionString);
        }

        public string Insert(Producto producto)
        {
            return productoRepositorio.Insert(producto);
        }

        public List<Producto> obtenerProductos() 
        {

            return productoRepositorio.obtenerProductos();
        
        }

        public List<ProductoDTO> obtenerProductosDTO() 
        {
            List<ProductoDTO> productosDTO = new List<ProductoDTO>();
            List<Producto> productos = obtenerProductos();
            foreach (Producto producto in productos) 
            {
                ProductoDTO proDTO = new ProductoDTO();
                proDTO.ID = producto.idProducto;
                proDTO.PRODUCTO = producto.nombreProducto;
                proDTO.PRECIO = producto.precio + "";
                productosDTO.Add(proDTO);
            
            }

            return productosDTO;
        }

        public string obtenerSiguienteID() 
        {
            List<Producto> productos = obtenerProductos();
            int id = 1;

            if (productos.Count > 0) 
            {
                id = Int32.Parse(productos[0].idProducto) + 1;
            }

            return id + "";


        }

        public Producto obtenerProductoPorID(string id) 
        {
            Producto producto = new Producto();
            List<Producto> productos = obtenerProductos();
            foreach (Producto product in productos) 
            {

                if (product.idProducto.Equals(id)) 
                {
                
                producto = product;
                }
            
            }

            return producto;


        }

        public string Update(Producto producto) 
        {

            return productoRepositorio.Update(producto);
        }
    }
}
