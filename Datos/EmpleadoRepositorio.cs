using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class EmpleadoRepositorio : ConnectionManager
    {
        public EmpleadoRepositorio(string connectionString) : base(connectionString)
        {
        }

        public string EnviarRegistro(string user, DateTime actual)
        {
            Open();
            OracleCommand command = new OracleCommand("InsertarRegistroIngreso", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("v_cedula", OracleType.VarChar).Value = user;
            command.Parameters.Add("v_fechaIngreso", OracleType.DateTime).Value = actual;
            int r = command.ExecuteNonQuery();
            Close();
            return r + "";
        }

        public int IniciarSesion(string usuario, string contraseña) 
        {
            Open();
            OracleCommand command = new OracleCommand("iniciar_sesion", conexion);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros de entrada
            command.Parameters.Add("p_usuario", OracleType.VarChar).Value = usuario;
            command.Parameters.Add("p_contraseña", OracleType.VarChar).Value = contraseña;

 
            OracleParameter resultadoParam = new OracleParameter();
            resultadoParam.ParameterName = "RETURN_VALUE";
            resultadoParam.Direction = ParameterDirection.ReturnValue;
            resultadoParam.OracleType = OracleType.Int32;
            command.Parameters.Add(resultadoParam);

 
            command.ExecuteNonQuery();
            Close();

            int inicioSesionExitoso = Convert.ToInt32(resultadoParam.Value);
            return inicioSesionExitoso;
           
          
            

        }
    }
}
