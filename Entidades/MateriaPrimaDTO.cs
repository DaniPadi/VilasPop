using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MateriaPrimaDTO
    {
        public MateriaPrimaDTO()
        {
        }

        public MateriaPrimaDTO(string iD, string nOMBRE, string cADUCIDAD, string aLMACEN)
        {
            ID = iD;
            NOMBRE = nOMBRE;
            CADUCIDAD = cADUCIDAD;
            ALMACEN = aLMACEN;
        }

        public string ID { get; set; }
        public string NOMBRE { get; set; }
        public string CADUCIDAD { get; set;}
        public string ALMACEN { get; set;}
    }
}
