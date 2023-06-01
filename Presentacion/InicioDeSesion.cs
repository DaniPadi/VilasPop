using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Logica;
using Org.BouncyCastle.Asn1.Crmf;

namespace Presentacion
{
    public partial class InicioDeSesion : Form
    {
        bool adminMode = false;
        string User ;
        string Password ;
     

        EmpleadoServicio servicioEmpleado = new EmpleadoServicio(ConfigConnection.connectionString);
        VentanaPrincipal formulario2 = new VentanaPrincipal();
        public InicioDeSesion()
        {
            InitializeComponent();
            if (adminMode == true) {

                this.Hide();
                txtPass.Text = "";
                txtUser.Text = "";
                formulario2.ShowDialog();
                this.Show();
            }

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
             User = txtUser.Text;
             Password = txtPass.Text;
            if (adminMode)
            {
              
            }
            else 
            {
                if (servicioEmpleado.iniciarSesion(User, Password))
                {
                    DateTime actual = DateTime.Now;
                    string msg = servicioEmpleado.EnviarRegistro(User, actual);
                    Console.WriteLine(msg);
                    MessageBox.Show("Sesión Iniciada", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    txtPass.Text = "";
                    txtUser.Text = "";
                    formulario2.ShowDialog();
                    this.Show();

                }
                else 
                {
                    MessageBox.Show("Inicio de Sesión invalido", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Text = "";
                    txtUser.Text = "";

                } 
            }
        }

        private void InicioDeSesion_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.ControlKey)
            {
                adminMode = true;
            }
        }

        private void InicioDeSesion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                adminMode = false;
            }
        }

        private void InicioDeSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
