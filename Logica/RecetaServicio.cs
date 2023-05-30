using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Logica
{
    public class RecetaServicio
    {
        RecetaRepositorio recetaRepositorio;

        public RecetaServicio(string ConnectioString)
        {
            recetaRepositorio = new RecetaRepositorio(ConnectioString);

        }

        public string Insert(Receta receta) 
        {
            return recetaRepositorio.Insert(receta);
        
        }
        public List<Receta> obtenerRecetas() 
        {
        return recetaRepositorio.obtenerRecetas();
        }
        public string obtenerSiguienteID() 
        {
            int id = 1;
            List<Receta> recetas = obtenerRecetas();

            if (recetas.Count > 0) 
            {
            id = Int32.Parse(recetas[0].id) + 1;
            }

            return id + "";


        }

        public Receta buscarRecetaDeProducto(string idProd) 
        {
            Receta receta = new Receta();
            foreach (Receta rece in obtenerRecetas()) 
            {
                if (rece.productoID.Equals(idProd)) 
                {

                    receta = rece;
                    break;
                }
            }
            return receta;
        
        
        }
    }
}
