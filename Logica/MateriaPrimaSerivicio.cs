using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
namespace Logica
{
    public class MateriaPrimaSerivicio
    {
        materiaPrimaRepositorio MateriaPrepositorio;
        public MateriaPrimaSerivicio(String connectionstring)
        {
            MateriaPrepositorio = new materiaPrimaRepositorio(connectionstring);
        }

        public string Insert(MateriaPrima materiaP) 
        {

        return MateriaPrepositorio.Insert(materiaP);

        }

        public int ObtenerCantidad() 
        {
        return obtenerMateriasPrimas().Count();

        }

        public List<MateriaPrima> obtenerMateriasPrimas() 
        {

            return MateriaPrepositorio.obtenerMateriasPrimas();
        }

        public int obtenerSiguienteCodigo() 
        {
            List<MateriaPrima> materiasPrimas = obtenerMateriasPrimas();
    
           int ultimoId = 1;

            if (materiasPrimas.Count > 0)
            {
                ultimoId = Int32.Parse(materiasPrimas[0].idMateriaPrima) + 1;
            }

            return ultimoId;

        }

        public List<MateriaPrimaDTO> obtenerDTO() 
        
        {
            List<MateriaPrimaDTO> materiasPDTO = new List<MateriaPrimaDTO>();
            List<MateriaPrima> materiasP = obtenerMateriasPrimas();
            foreach (MateriaPrima materia in materiasP) 
            {
                MateriaPrimaDTO dto = new MateriaPrimaDTO();
                dto.ID = materia.idMateriaPrima.ToString();
                dto.NOMBRE = materia.nombreMateriaPrima;
                dto.CADUCIDAD = materia.fechaCaducidad.ToShortDateString();
                if (materia.gramos > 0)
                {
                    dto.ALMACEN = materia.gramos + " gr";
                }
                else if (materia.mililitros > 0)
                {
                    dto.ALMACEN = materia.mililitros + " ml";
                }
                else 
                {
                    dto.ALMACEN = materia.unidades + " ud";
                }
            
                
                materiasPDTO.Add(dto);

            
            }


            return materiasPDTO;
        }

        public MateriaPrima obtenerMateriaPrimaConID(int idMP)
        {
            List<MateriaPrima> materiasP = obtenerMateriasPrimas();
            foreach (MateriaPrima mp in materiasP) 
            {
                Console.WriteLine("id: " + idMP + " id2: " + mp.idMateriaPrima);
                if (mp.idMateriaPrima.Equals(idMP+"")) 
                {
                return mp;
                }
            }

            return null;
        }

        public MateriaPrima obtenerMateriaPrimaConNombre(string nombre)
        {
            List<MateriaPrima> materiasP = obtenerMateriasPrimas();
            foreach (MateriaPrima mp in materiasP)
            {
               
                if (mp.nombreMateriaPrima.Equals(nombre))
                {
                    return mp;
                }
            }

            return null;
        }


        public string Update(List<MateriaPrima> materiasP) 
        {
            int count = 0;
            foreach (MateriaPrima materia in materiasP) 
            {

                count =MateriaPrepositorio.Update(materia);
            }

            return count.ToString();
        
        }
    }
}
