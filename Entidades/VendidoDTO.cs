using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class VendidoDTO
    {
        public VendidoDTO()
        {
        }

        public VendidoDTO(string pRODUCTO, string fACTURA, int cANTIDAD, float vALOR)
        {
            PRODUCTO = pRODUCTO;
            FACTURA = fACTURA;
            CANTIDAD = cANTIDAD;
            VALOR = vALOR;
        }

        public string PRODUCTO { get; set; }
        public string FACTURA { get; set; }
        public int CANTIDAD { get; set; }
        public float VALOR { get; set; }
    }
}
