using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public  class FacturaDTO
    {
        public FacturaDTO()
        {
        }

        public FacturaDTO(string cODIGO, string fECHA, float cOSTO, string cLIENTE, string mETODO)
        {
            CODIGO = cODIGO;
            FECHA = fECHA;
            COSTO = cOSTO;
            CLIENTE = cLIENTE;
            METODO = mETODO;
        }

        public string CODIGO { get; set; }
        public string FECHA { get; set; }
        public float COSTO { get; set; }
        public string CLIENTE { get; set; }
        public string METODO { get; set; }
    }
}
