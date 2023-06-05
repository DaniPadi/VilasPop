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

        public string Insert(Cliente clienteNuevo)
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_cliente", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_cedula", OracleType.VarChar).Value = clienteNuevo.cedula;
            command.Parameters.Add("v_nombres", OracleType.VarChar).Value = clienteNuevo.nombre;
            command.Parameters.Add("v_apellidos", OracleType.VarChar).Value = clienteNuevo.apellido;
            command.Parameters.Add("v_celular", OracleType.VarChar).Value = clienteNuevo.telefono;
            command.Parameters.Add("v_correo", OracleType.VarChar).Value = clienteNuevo.correo;

            int r = command.ExecuteNonQuery();
            Close();
            return "1";
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

        public Cliente BuscarCliente(string id)
        {
            Cliente cliente = new Cliente();

            OracleCommand command = new OracleCommand("obtener_cliente", conexion);
            command.CommandType= CommandType.StoredProcedure;

            OracleParameter resultCursor = new OracleParameter();
            resultCursor.ParameterName = "result_cursor";
            resultCursor.OracleType = OracleType.Cursor;
            resultCursor.Direction = ParameterDirection.ReturnValue;

            OracleParameter Idcedula = new OracleParameter();
            Idcedula.ParameterName = "v_id_cliente";
            Idcedula.OracleType = OracleType.VarChar;
            Idcedula.Value = id;

            command.Parameters.Add(resultCursor);
            command.Parameters.Add(Idcedula);

            Open();

            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                cliente.cedula = reader.GetString(0);
                cliente.nombre = reader.GetString(1);
                cliente.apellido = reader.GetString(2);
                cliente.telefono = reader.GetString(3);
                cliente.correo = reader.GetString(4);
            }

            reader.Close();
            Close();

            return cliente;
        }
    }
}
