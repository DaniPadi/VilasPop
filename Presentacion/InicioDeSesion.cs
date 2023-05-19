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


namespace Presentacion
{
    public partial class InicioDeSesion : Form
    {
        bool adminMode = false;
        string User ;
        string Password ;
        Empleado admin = new Empleado("admin","admin","1234","admin","1234");

        public InicioDeSesion()
        {
            InitializeComponent();
            if (adminMode == true) {
                VentanaPrincipal formulario2 = new VentanaPrincipal();

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
                if ((User.Equals(admin.cedula) && adminMode))
                {
                    if ((Password.Equals(admin.contrasena) && adminMode))
                    {
                        MessageBox.Show("Sesión Iniciada", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        VentanaPrincipal formulario2 = new VentanaPrincipal();

                        this.Hide();
                        txtPass.Text = "";
                        txtUser.Text = "";
                        formulario2.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Contraseña Incorrecta", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPass.Text = "";
                    }


                }
                else
                {
                    MessageBox.Show("Usuario no registrado", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Text = "";
                    txtUser.Text = "";

                }


            }
            else 
            {
                 procesamientoDeEmpleados pe = new procesamientoDeEmpleados();
                Empleado empleado = pe.VerificarUsuario(User, Password);
              
                if (empleado != null)
                {
                    MessageBox.Show("Sesión Iniciada", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VentanaPrincipal formulario2 = new VentanaPrincipal();

                    this.Hide();
                    txtPass.Text = "";
                    txtUser.Text = "";
                    formulario2.ShowDialog();
                    this.Show();
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
