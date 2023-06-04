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
        materiaPrimaRepositorio materiaPrimaRepositorio;
        public MateriaPrimaSerivicio(String connectionstring)
        {
            materiaPrimaRepositorio = new materiaPrimaRepositorio(connectionstring);
        }

        public string Insert(MateriaPrima materiaP) 
        {

        return materiaPrimaRepositorio.Insert(materiaP);

        }

        public int ObtenerCantidad() 
        {
        return obtenerMateriasPrimas().Count();

        }

        public List<MateriaPrima> obtenerMateriasPrimas() 
        {

            return materiaPrimaRepositorio.obtenerMateriasPrimas();
        }
        public List<MateriaPrima> obtenerMateriasPrimasValidas()
        {

            return materiaPrimaRepositorio.obtenerMateriasPrimasValidas();
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
        public List<MateriaPrimaDTO> obtenerDTOValido()

        {
            List<MateriaPrimaDTO> materiasPDTO = new List<MateriaPrimaDTO>();
            List<MateriaPrima> materiasP = obtenerMateriasPrimasValidas();
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


        public MateriaPrima obtenerMateriaPrimaConID(string idMP)
        {
            List<MateriaPrima> materiasP = obtenerMateriasPrimas();
            foreach (MateriaPrima mp in materiasP) 
            {
               
                if (mp.idMateriaPrima.Equals(idMP)) 
                {
                return mp;
                }
            }

            return null;
        }

        public MateriaPrima obtenerMateriaPrimaConNombre(string nombre)
        {
            List<MateriaPrima> materiasP = obtenerMateriasPrimasValidas();

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

                count =materiaPrimaRepositorio.Update(materia);
            }

            return count.ToString();
        
        }
        public string UpdateCero(int materiaId) 
        {
        
        return materiaPrimaRepositorio.UpdateCero(materiaId);
        }

        public string Eliminar(MateriaPrima materiaP) 
        {
        
        return materiaPrimaRepositorio.Eliminar(materiaP);
        }
    }
}
