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
        string connection = "";
        public IngredienteServicio(string ConnectioString)
        {
            ingredienteRepositorio = new IngredienteRepositorio(ConnectioString);
            connection = ConnectioString;
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
        public List<IngredienteDTO> convertirDTO(List<Ingrediente> ingredientes)
        {
            List<IngredienteDTO> ingredientesDTO = new List<IngredienteDTO>();
            MateriaPrimaSerivicio servicioMateriaP = new MateriaPrimaSerivicio(connection); 
            foreach (Ingrediente ingrediente in ingredientes) 
            {
                IngredienteDTO ingredienteDTO = new IngredienteDTO();
                ingredienteDTO.RECETA = ingrediente.idReceta;
                ingredienteDTO.MATERIA_PRIMA = servicioMateriaP.obtenerMateriaPrimaConID(ingrediente.idmateriaPrima).nombreMateriaPrima;
                if (ingrediente.gramos > 0)
                {
                    ingredienteDTO.CANTIDAD = ingrediente.gramos;
                    ingredienteDTO.UNIDAD = "gr";
                }
                else if (ingrediente.mililitros > 0)
                {
                    ingredienteDTO.CANTIDAD = ingrediente.mililitros;
                    ingredienteDTO.UNIDAD = "ml";
                }
                else 
                {
                    ingredienteDTO.CANTIDAD = ingrediente.unidades;
                    ingredienteDTO.UNIDAD = "ud";
                }
                ingredientesDTO.Add(ingredienteDTO);


            }

            return ingredientesDTO;
        }
        public List<Ingrediente> convertirDTOaNormal(List<IngredienteDTO> ingredientesDTO)
        {
            List<Ingrediente> ingredientes = new List<Ingrediente>();
            MateriaPrimaSerivicio servicioMateriaP = new MateriaPrimaSerivicio(connection);

            foreach (IngredienteDTO ingredienteDTO in ingredientesDTO)
            {
                Ingrediente ingrediente = new Ingrediente();
                ingrediente.idReceta = ingredienteDTO.RECETA;

                string nombreMateriaPrima = ingredienteDTO.MATERIA_PRIMA;
                MateriaPrima materiaPrima = servicioMateriaP.obtenerMateriaPrimaConNombre(nombreMateriaPrima);
                if (materiaPrima != null)
                {
                    ingrediente.idmateriaPrima = materiaPrima.idMateriaPrima;
                }
                

                string unidad = ingredienteDTO.UNIDAD;
                float cantidad = ingredienteDTO.CANTIDAD;

                if (unidad == "gr")
                {
                    ingrediente.gramos = cantidad;
                }
                else if (unidad == "ml")
                {
                    ingrediente.mililitros = cantidad;
                }
                else if (unidad == "ud")
                {
                    ingrediente.unidades = (int)cantidad;
                }

                ingredientes.Add(ingrediente);
            }

            return ingredientes;
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
