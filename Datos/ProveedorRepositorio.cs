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
    public class ProveedorRepositorio : ConnectionManager
    {
        public ProveedorRepositorio(string connectionString) : base(connectionString)
        {

        }
       

        public string Insert(Proveedor proveedor)
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_proveedor", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_proveedor", OracleType.VarChar).Value = proveedor.id;
            command.Parameters.Add("v_nombre_proveedor", OracleType.VarChar).Value = proveedor.nombre;
            command.Parameters.Add("v_telefono_proveedor", OracleType.VarChar).Value = proveedor.telefono;
            command.Parameters.Add("v_correo", OracleType.VarChar).Value = proveedor.correo;
            command.ExecuteNonQuery();
            Close();
            return  "1";
        }

        public List<Proveedor> obtenerProveedores() 
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            OracleCommand command = new OracleCommand("obtener_proveedores", conexion);
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
                Proveedor proveedor = new Proveedor();
                proveedor.id = reader.GetString(0);
                proveedor.nombre = reader.GetString(1);
                proveedor.telefono = reader.GetString(2);
                proveedor.correo = reader.GetString(3);

                proveedores.Add(proveedor);
            }

            reader.Close();
            Close();

           



            return proveedores;
        }
    }
}
