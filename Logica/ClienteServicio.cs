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
        ClienteRepositorio clienteRepositorio;

        public ClienteServicio(string ConnectionString)
        {
            this.clienteRepositorio = new ClienteRepositorio(ConnectionString);
        }

        public Cliente BuscarCliente(string text)
        {
            List<Cliente> clientes = ObtenerCLientes();
            foreach(Cliente cliente in clientes) 
            {
                if (cliente.cedula.Equals(text)) 
                {
                return cliente;
                }
            }

            return null;
        }

        public string Insert(Cliente clienteNuevo)
        {
            return clienteRepositorio.Insert(clienteNuevo);
        }

        public List<Cliente> ObtenerCLientes()
        {
            return clienteRepositorio.ObtenerCLientes();
        
        }
        }
}
