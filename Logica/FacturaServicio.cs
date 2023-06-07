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
        string connection;
        public FacturaServicio(string ConnectionString)
        {
            this.facturaRepositorio = new FacturaRepositorio(ConnectionString);
            connection = ConnectionString;
        }

        public string Insert(Factura factura) 
        {
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

            public List<FacturaDTO> convertirDTO(List<Factura> facturas) 
            {
                List<FacturaDTO> facturasDTO = new List<FacturaDTO>();

                foreach (Factura factura in facturas) 
                {
                FacturaDTO facturaDTO = new FacturaDTO();
                    facturaDTO.CODIGO = factura.id_factura;
                    facturaDTO.FECHA = factura.fecha.ToLongDateString();
                    facturaDTO.CLIENTE = factura.cliente.cedula;
                    facturaDTO.COSTO = factura.precioTotal;
                    facturaDTO.METODO = "efectivo";
                    facturasDTO.Add(facturaDTO);
            
                }

                return facturasDTO;


            }
        public List<Factura> convertirDTOaNormal(List<FacturaDTO> facturasDTO)
        {
            List<Factura> facturas = new List<Factura>();
            ClienteServicio servicioCLiente = new ClienteServicio(connection);
            foreach (FacturaDTO facturaDTO in facturasDTO)
            {
                Factura factura = new Factura();

                factura.id_factura = facturaDTO.CODIGO;
                factura.fecha = DateTime.Parse(facturaDTO.FECHA);
                Cliente cliente = servicioCLiente.BuscarCliente(facturaDTO.CLIENTE);
                factura.cliente = cliente;
                factura.precioTotal = facturaDTO.COSTO;
                factura.IdMetodo = "1";

                facturas.Add(factura);
            }

            return facturas;
        }
    }
}
