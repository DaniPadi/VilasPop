using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class EmpleadoServicio
    {
        EmpleadoRepositorio empleadoRepositorio;

        public EmpleadoServicio(string ConnectionString)
        {
            this.empleadoRepositorio = new EmpleadoRepositorio(ConnectionString);

        }

        public string EnviarRegistro(string user, DateTime actual)
        {
            return empleadoRepositorio.EnviarRegistro(user,actual);
        }

        public bool iniciarSesion(string usuario, string contraseña) 
        {
            int result = empleadoRepositorio.IniciarSesion(usuario, contraseña);
            if (result == 1)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
