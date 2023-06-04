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
    public class IngredienteRepositorio : ConnectionManager

    {
        public IngredienteRepositorio(string connectionString) : base(connectionString)
        {
        }

        public int Insert(Ingrediente ingrediente) 
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_ingrediente", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_receta", OracleType.Number).Value = Int32.Parse(ingrediente.idReceta);
            command.Parameters.Add("v_id_materiap", OracleType.Number).Value = Int32.Parse(ingrediente.idmateriaPrima);
            command.Parameters.Add("v_gramos", OracleType.Number).Value = ingrediente.gramos;
            command.Parameters.Add("v_mililitros", OracleType.Number).Value = ingrediente.mililitros;
            command.Parameters.Add("v_unidades", OracleType.Number).Value = ingrediente.unidades;
            int r = command.ExecuteNonQuery();
            Close();
            return r ;

        }

        public List<Ingrediente> obtenerIngredientesPorReceta(string RecetaId) 
        {
            List<Ingrediente> ingredientes = new List<Ingrediente>();

            OracleCommand command = new OracleCommand("obtener_ingredientes_por_receta", conexion);
            command.CommandType = CommandType.StoredProcedure;

            OracleParameter resultCursor = new OracleParameter();
            resultCursor.ParameterName = "result_cursor";
            resultCursor.OracleType = OracleType.Cursor;
            resultCursor.Direction = ParameterDirection.ReturnValue;

            OracleParameter paramReceta = new OracleParameter();
            paramReceta.ParameterName = "v_id_receta";
            paramReceta.OracleType = OracleType.Number;
            paramReceta.Value = Int32.Parse(RecetaId);

            command.Parameters.Add(resultCursor);
            command.Parameters.Add(paramReceta);
 

            Open();
            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Ingrediente ingrediente = new Ingrediente();
                ingrediente.idReceta = reader.GetInt32(0).ToString();
                ingrediente.idmateriaPrima = reader.GetInt32(1).ToString();
                ingrediente.gramos = reader.GetFloat(2);
                ingrediente.mililitros = reader.GetFloat(3);
                ingrediente.unidades = reader.GetInt32(4);
                ingredientes.Add(ingrediente);
            }

            reader.Close();
            Close();

            return ingredientes;

        }

        public int reEnfocarProductos(int idNuevo, int idViejo) 
        {
            Open();
            OracleCommand command = new OracleCommand("reemplazar_materiaprima", conexion);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add("v_id_materiap_new", OracleType.Number).Value = idNuevo;
            command.Parameters.Add("v_id_materiap_old", OracleType.Number).Value = idViejo;

     
            int r = command.ExecuteNonQuery();
            Close();

            return r;
        }

        public string Eliminar(Ingrediente ingrediente) 
        {

            Open();
            OracleCommand command = new OracleCommand("eliminar_ingrediente", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("v_id_receta", OracleType.Number).Value = ingrediente.idReceta;
            command.Parameters.Add("v_id_materiap", OracleType.Number).Value = ingrediente.idmateriaPrima;

            int r = command.ExecuteNonQuery();
            Close();

            return r + "";

        }
    }
}
