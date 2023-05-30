using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Logica
{
    public class GastoServicio
    {
        GastoRepositorio gastoRepositorio;

        public GastoServicio(string connectionString)
        {
            gastoRepositorio = new GastoRepositorio(connectionString);
        }

        public string Insert(Gasto gasto ) 
        {


            return gastoRepositorio.Insert(gasto);
        }
    }
}
