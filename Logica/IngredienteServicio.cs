using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class IngredienteServicio
    {
        IngredienteRepositorio ingredienteRepositorio;

        public IngredienteServicio(string ConnectioString)
        {
            ingredienteRepositorio = new IngredienteRepositorio(ConnectioString);
        }

        public string Insert(List<Ingrediente> ingredientes) 
        {
            int cont = 0;
            foreach (Ingrediente ingrediente in ingredientes) 
            {

                cont += ingredienteRepositorio.Insert(ingrediente);
            
            }

            return cont + "";
        
        }

        public List<Ingrediente> obtenerIngredientesPorReceta(string receta) 
        {

            return ingredienteRepositorio.obtenerIngredientesPorReceta(receta);
        }

        public int reEnfocarProductos(int idNuevo, int idViejo) 
        {

            return ingredienteRepositorio.reEnfocarProductos(idNuevo, idViejo);
        }
        public string Eliminar(Ingrediente ingrediente) 
        {

            return ingredienteRepositorio.Eliminar(ingrediente);
        }


    }
}
