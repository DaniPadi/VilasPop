using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ProductoDTO
    {
        public ProductoDTO()
        {
        }

        public ProductoDTO(string iD, string pRODUCTO, string pRECIO)
        {
            ID = iD;
            PRODUCTO = pRODUCTO;
            PRECIO = pRECIO;
        }

        public string ID { get; set; }
        public string PRODUCTO { get; set; }
        public string PRECIO { get; set; }
    }
}
