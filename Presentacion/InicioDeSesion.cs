﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
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

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
             User = txtUser.Text;
             Password = txtPass.Text;
            if (User.Equals(admin.cedula) && adminMode)
            {
                if (Password.Equals(admin.contrasena) && adminMode)
                {
                    MessageBox.Show("Sesión Iniciada", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VentanaPrincipal formulario2 = new VentanaPrincipal();
                    formulario2.Informacion = "Información que se va a pasar";
                    this.Hide();
                    txtPass.Text = "";
                    txtUser.Text = "";
                    formulario2.ShowDialog();
                    this.Show();
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
    }
}
