using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class materiaPrimaRepositorio : ConnectionManager
    {
        public materiaPrimaRepositorio(string connectionString) : base(connectionString)
        {
        }

        public string Insert(MateriaPrima materiaP) 
        {
            Open();
            OracleCommand command = new OracleCommand("ingresar_materias_primas", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_materiap", OracleType.Number).Value = Int32.Parse(materiaP.idMateriaPrima);
            command.Parameters.Add("v_nombremp", OracleType.VarChar).Value = materiaP.nombreMateriaPrima;
            command.Parameters.Add("v_fechacaducidad", OracleType.DateTime).Value = materiaP.fechaCaducidad;
            command.Parameters.Add("v_gramos", OracleType.Number).Value = materiaP.gramos;
            command.Parameters.Add("v_mililitros", OracleType.Number).Value = materiaP.mililitros;
            command.Parameters.Add("v_unidades", OracleType.Number).Value = materiaP.unidades;
            int r = command.ExecuteNonQuery();
            Close();
            return  "1";
        }


        public List<MateriaPrima> obtenerMateriasPrimas() 
        {
            List<MateriaPrima> materiasPrimas = new List<MateriaPrima>();
            OracleCommand command = new OracleCommand("obtener_materias_primas", conexion);
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
                MateriaPrima materiaP = new MateriaPrima();
                materiaP.idMateriaPrima = reader.GetInt32(0).ToString();
                materiaP.nombreMateriaPrima = reader.GetString(1);
                materiaP.fechaCaducidad = reader.GetDateTime(2);
                materiaP.gramos = reader.GetFloat(3);
                materiaP.mililitros = reader.GetFloat(4);
                materiaP.unidades = reader.GetInt32(5);

                materiasPrimas.Add(materiaP);
            }

            reader.Close();
            Close();
     
            return materiasPrimas;
        }
        public List<MateriaPrima> obtenerMateriasPrimasValidas()
        {
            List<MateriaPrima> materiasPrimas = new List<MateriaPrima>();
            OracleCommand command = new OracleCommand("obtener_materias_primas_validas", conexion);
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
                MateriaPrima materiaP = new MateriaPrima();
                materiaP.idMateriaPrima = reader.GetInt32(0).ToString();
                materiaP.nombreMateriaPrima = reader.GetString(1);
                materiaP.fechaCaducidad = reader.GetDateTime(2);
                materiaP.gramos = reader.GetFloat(3);
                materiaP.mililitros = reader.GetFloat(4);
                materiaP.unidades = reader.GetInt32(5);
                materiasPrimas.Add(materiaP);
            }

            reader.Close();
            Close();

            return materiasPrimas;
        }


        public int Update(MateriaPrima materia)
        {
            Open();
            OracleCommand command = new OracleCommand("actualizar_materias_primas", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("v_id_materiap", OracleType.Number).Value = Int32.Parse(materia.idMateriaPrima);
            command.Parameters.Add("v_gramos", OracleType.Number).Value = materia.gramos;
            command.Parameters.Add("v_mililitros", OracleType.Number).Value = materia.mililitros;
            command.Parameters.Add("v_unidades", OracleType.Number).Value = materia.unidades;

            
            int r = command.ExecuteNonQuery();
            Close();
            return r;
        }
        public string UpdateCero(int materiaId)
        {
            Open();
            OracleCommand command = new OracleCommand("actualizar_materia_cero", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("v_id_materiap", OracleType.Number).Value = materiaId;
            int r = command.ExecuteNonQuery();
            Close();
            return r + "";
        }

        public string Eliminar(MateriaPrima materiaP)
        {

            Open();
            OracleCommand command = new OracleCommand("eliminar_materia_prima", conexion);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("v_id_materiap", OracleType.Number).Value = materiaP.idMateriaPrima;

            int r = command.ExecuteNonQuery();
            Close();

            return r + "";

        }
    }
}
