using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string User = txtUser.Text;
            string Password = txtPass.Text;
            if (User.Equals("admin"))
            {
                if (Password.Equals("1234"))
                {
                    MessageBox.Show("Sesión Iniciada", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else { 
                    MessageBox.Show("Contraseña Incorrecta", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Text = "";
                }


            }
            else {
                MessageBox.Show("Usuario no registrado", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Text = "";
                txtUser.Text = "";

            }



        }
    }
}
