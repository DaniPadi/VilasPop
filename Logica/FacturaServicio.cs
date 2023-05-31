using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Logica
{
    public class FacturaServicio
    {
        FacturaRepositorio facturaRepositorio;

        public FacturaServicio(string ConnectionString)
        {
            this.facturaRepositorio = new FacturaRepositorio(ConnectionString);

        }

        public string Insert(Factura factura) 
        {
            Console.WriteLine("Estoy aquí");
            return facturaRepositorio.Insert(factura);
        }

        public List<Factura> obtenerFacturas() 
        {
        return facturaRepositorio.obtenerFacturas();
        }

        public int obtenerCodigoFactura() 
        {
        return obtenerFacturas().Count() + 1;
        }

        public List<Factura> obtenerFacturasDesdeHasta(DateTime inicio, DateTime fin)
        {
            return facturaRepositorio.obtenerFacturasDesdeHasta(inicio, fin);
        }

        public FacturasInfo infoFacturas(List<Factura> facturas) 
        {
            int facts = facturas.Count;
            double dinero = 0;
            foreach (Factura factura in facturas) 
            {
                dinero += factura.precioTotal;
            
            }

            return new FacturasInfo(facts, dinero);
        

        }
    }
}
