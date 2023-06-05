using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Datos
{
    public class FacturaRepositorio : ConnectionManager
    {
        public FacturaRepositorio(string connectionString) : base(connectionString)
        {
        }

        public List<Factura> obtenerFacturasDesdeHasta(DateTime inicio, DateTime fin)
        {
            List<Factura> facturas = new List<Factura>();

            OracleCommand command = new OracleCommand("obtener_facturas_por_rango_fecha", conexion);
            command.CommandType = CommandType.StoredProcedure;

            OracleParameter resultCursor = new OracleParameter();
            resultCursor.ParameterName = "result_cursor";
            resultCursor.OracleType = OracleType.Cursor;
            resultCursor.Direction = ParameterDirection.ReturnValue;

            OracleParameter paramInicio = new OracleParameter();
            paramInicio.ParameterName = "v_fecha_inicio";
            paramInicio.OracleType = OracleType.DateTime;
            paramInicio.Value = inicio;

            OracleParameter paramFin = new OracleParameter();
            paramFin.ParameterName = "v_fecha_fin";
            paramFin.OracleType = OracleType.DateTime;
            paramFin.Value = fin;

            command.Parameters.Add(resultCursor);
            command.Parameters.Add(paramInicio);
            command.Parameters.Add(paramFin);

            Open();
            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Factura factura = new Factura();
                factura.id_factura = reader.GetString(0);
                factura.fecha = reader.GetDateTime(1);
                factura.precioTotal = reader.GetFloat(2);
                factura.cliente = new ClienteRepositorio(conexion.ConnectionString).BuscarCliente(reader.GetString(3));
                factura.IdMetodo = reader.GetInt32(4).ToString();
                facturas.Add(factura);
            }

            reader.Close();
            Close();

            return facturas;
        }

        public string Insert(Factura factura)
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_factura", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_factura", OracleType.VarChar).Value = factura.id_factura;
            command.Parameters.Add("v_fechafactura", OracleType.DateTime).Value = factura.fecha;
            command.Parameters.Add("v_preciototal", OracleType.Number).Value = factura.precioTotal;
            command.Parameters.Add("v_cliente", OracleType.VarChar).Value = factura.cliente.cedula;
            command.Parameters.Add("v_id_metodo", OracleType.Number).Value = Int32.Parse(factura.IdMetodo);
            command.ExecuteNonQuery();
            Close();
            return "1";
        }

        public List<Factura> obtenerFacturas() 
        {
            List<Factura> facturas= new List<Factura>();

            OracleCommand command = new OracleCommand("obtener_facturas", conexion);
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
                Factura factura = new Factura();
                factura.id_factura = reader.GetString(0);
                factura.fecha = reader.GetDateTime(1);
                factura.precioTotal = reader.GetFloat(2);
                factura.cliente= new ClienteRepositorio(conexion.ConnectionString).BuscarCliente(reader.GetString(3));
                factura.IdMetodo = reader.GetInt32(4).ToString();
                facturas.Add(factura);
            }

            reader.Close();
            Close();

            return facturas;
        }
    }
}
