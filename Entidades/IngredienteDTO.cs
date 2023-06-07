using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class IngredienteDTO
    {
        public IngredienteDTO()
        {
        }

        public IngredienteDTO(string rECETA, string mATERIA_PRIMA, float cANTIDAD, string uNIDAD)
        {
            RECETA = rECETA;
            MATERIA_PRIMA = mATERIA_PRIMA;
            CANTIDAD = cANTIDAD;
            UNIDAD = uNIDAD;
        }

        public string RECETA { get; set; }
        public string MATERIA_PRIMA { get; set; }
        public float CANTIDAD { get; set; }

        public string UNIDAD { get; set; }

    }


}

