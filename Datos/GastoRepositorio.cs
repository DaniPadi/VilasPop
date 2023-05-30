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
    public class GastoRepositorio : ConnectionManager
    {
        public GastoRepositorio(string connectionString) : base(connectionString)
        {

        }

        public string Insert(Gasto gasto)
        {
            Open();

           
            OracleCommand command = new OracleCommand("ingresar_gastos", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_gasto", OracleType.VarChar).Value = gasto.idGasto;
            command.Parameters.Add("v_gasto", OracleType.Number).Value = gasto.costo;
            command.Parameters.Add("v_id_proveedor", OracleType.VarChar).Value = gasto.proveedor.id;
            command.Parameters.Add("v_id_materiap", OracleType.Number).Value = gasto.materiaPrima.idMateriaPrima;
            command.Parameters.Add("v_fechagasto", OracleType.DateTime).Value = gasto.fecha;
            command.Parameters.Add("v_gramost", OracleType.Number).Value = gasto.gramos;
            command.Parameters.Add("v_mililitrost", OracleType.Number).Value = gasto.mililitros;
            command.Parameters.Add("v_unidadest", OracleType.Number).Value = gasto.unidades;
            command.ExecuteNonQuery();
            Close();
            return "1";
        }

    }
}
