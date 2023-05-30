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
    public class RecetaRepositorio : ConnectionManager
    {
        public RecetaRepositorio(string connectionString) : base(connectionString)
        {
        }

        public string Insert(Receta receta) 
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_receta", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_receta", OracleType.Number).Value = Int32.Parse(receta.id);
            command.Parameters.Add("v_receta", OracleType.VarChar).Value = receta.receta;
            command.Parameters.Add("v_id_producto", OracleType.Number).Value = Int32.Parse(receta.productoID);
            command.Parameters.Add("v_descripcion", OracleType.VarChar).Value = receta.descripcion;
            int r = command.ExecuteNonQuery();
            Close();
            return "1";


        }

        public List<Receta> obtenerRecetas() 
        {
            List<Receta> recetas = new List<Receta>();

            OracleCommand command = new OracleCommand("obtener_recetas", conexion);
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
                
                Receta receta = new Receta();
                receta.id = reader.GetInt32(0).ToString();
                receta.receta= reader.GetString(1);
                receta.productoID = reader.GetInt32(2).ToString();
                receta.descripcion = reader.GetString(3);
              
                recetas.Add(receta);
            }
            reader.Close();
            Close();

            return recetas;

        }


    }
}
