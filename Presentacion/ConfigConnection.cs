using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    public static class ConfigConnection
    {
        public static string connectionString = 
            ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }
}
