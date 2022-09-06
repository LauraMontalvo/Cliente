using Cliente.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Cliente.Web.API;

namespace Cliente
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = await WebApiLogin.PostValidarClave(new LoginRequest { Usuario = textBox1.Text, Password = textBox2.Text });
                if (usuario != null)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    MessageBox.Show("Bienvenido " + usuario.nombre,"Ingreso de sesion exitoso!",buttons,icon);
                }
                switch (usuario.idRol)
                {
                    case 1:
                        var adminForm = new AdminForm(usuario);
                        this.Hide();
                        adminForm.ShowDialog();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Show();
                        break;
                    case 2:
                        var formGestion = new GestionClienteForm(usuario);
                        this.Hide();
                        formGestion.ShowDialog();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Show();
                        break;
                    case 3:
                        var formDoctor = new CitasForm(usuario, DateTime.Now);
                        this.Hide();
                        formDoctor.ShowDialog();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Show();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
