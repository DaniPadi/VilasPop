using Entidades;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class VentaRepositorio : ConnectionManager
    {
        public VentaRepositorio(string connectionString) : base(connectionString)
        {
        }

        public int Insert(Vendidos venta) 
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_vendido", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_producto", OracleType.Number).Value = Int32.Parse(venta.id_producto);
            command.Parameters.Add("v_id_factura", OracleType.VarChar).Value = venta.id_factura;
            command.Parameters.Add("v_cantidad", OracleType.Number).Value = venta.cantidad;
            command.Parameters.Add("v_valor", OracleType.Number).Value = venta.valor;
            int r = command.ExecuteNonQuery();
            Close();
            return r;

        }

        public List<Vendidos> obtenerVentasConFactura(string id_factura) 
        {

            List<Vendidos> ventas = new List<Vendidos>();

            OracleCommand command = new OracleCommand("obtener_vendidos_por_factura", conexion);
            command.CommandType = CommandType.StoredProcedure;

            OracleParameter resultCursor = new OracleParameter();
            resultCursor.ParameterName = "result_cursor";
            resultCursor.OracleType = OracleType.Cursor;
            resultCursor.Direction = ParameterDirection.ReturnValue;

            OracleParameter paramFactura = new OracleParameter();
            paramFactura.ParameterName = "v_id_factura";
            paramFactura.OracleType = OracleType.VarChar;
            paramFactura.Value = id_factura;

            command.Parameters.Add(resultCursor);
            command.Parameters.Add(paramFactura);
          

            Open();
            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Vendidos venta = new Vendidos();
               venta.id_producto = reader.GetInt32(0).ToString();
                venta.id_factura = reader.GetString(1);
                venta.cantidad = reader.GetInt32(2);
                venta.valor = reader.GetFloat(3);
               ventas.Add(venta);
            }

            reader.Close();
            Close();

            return ventas;

        }
    }
}
