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
    public class ClienteRepositorio : ConnectionManager
    {
        public ClienteRepositorio(string connectionString) : base(connectionString)
        {
        }

        public List<Cliente> ObtenerCLientes()  
        {

            List<Cliente> clientes = new List<Cliente>();

            OracleCommand command = new OracleCommand("obtener_clientes", conexion);
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

                
                Cliente cliente = new Cliente();
                cliente.cedula = reader.GetString(0);
                cliente.nombre = reader.GetString(1);
                cliente.apellido = reader.GetString(2);
                cliente.telefono = reader.GetString(3);
                cliente.correo= reader.GetString(4);

               
                clientes.Add(cliente);
            }
            reader.Close();
            Close();

            return clientes;


        }
    }
}
