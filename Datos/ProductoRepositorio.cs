using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ProductoRepositorio : ConnectionManager
    {
        public ProductoRepositorio(string connectionString) : base(connectionString)
        {
        }

        public  string Insert(Producto producto)
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_producto", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_producto", OracleType.Number).Value = Int32.Parse(producto.idProducto);
            command.Parameters.Add("v_nombre_producto", OracleType.VarChar).Value = producto.nombreProducto;
            command.Parameters.Add("v_precio", OracleType.Number).Value = producto.precio;

            int r = command.ExecuteNonQuery();
            Close();
            return "1";
        }

        public List<Producto> obtenerProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            
            OracleCommand command = new OracleCommand("obtener_productos", conexion);
            command.CommandType = CommandType.StoredProcedure;

            OracleParameter resultCursor = new OracleParameter();
            resultCursor.ParameterName = "result_cursor";
            resultCursor.OracleType = OracleType.Cursor;
            resultCursor.Direction = ParameterDirection.ReturnValue;


            command.Parameters.Add(resultCursor);

            Open();
            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Producto producto = new Producto();
                producto.idProducto = reader.GetInt32(0).ToString();
                producto.nombreProducto = reader.GetString(1);
                producto.precio = reader.GetFloat(2);
                listaProductos.Add(producto);
            }

            Close();
            return listaProductos;
        }
    }
}
