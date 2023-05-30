using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ConnectionManager
    {
        protected OracleConnection conexion;
        public ConnectionManager(string connectionString)
        {
            conexion = new OracleConnection(connectionString);
        }
        public void Open()
        {
            conexion.Open();
            Console.WriteLine("Conexion abierta");
        }
        public void Close()
        {
            conexion.Close();
            Console.WriteLine("Conexion cerrada");
        }
    }
}
