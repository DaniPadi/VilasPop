using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ClienteServicio
    {
        ClienteRepositorio clienteServicio;

        public ClienteServicio(string ConnectionString)
        {
            this.clienteServicio = new ClienteRepositorio(ConnectionString);
        }

        public Cliente BuscarCliente(string text)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> ObtenerCLientes()
        {
            return clienteServicio.ObtenerCLientes();
        
        }
        }
}
