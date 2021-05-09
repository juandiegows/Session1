using Session1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public int Intento { get; set; }
        private void Login_Load(object sender, EventArgs e)
        {
            Intento = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(linkLabel1.Text == "Mostrar")
            {
                linkLabel1.Text = "Ocultar";
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {

                linkLabel1.Text = "Mostrar";
                textBox2.UseSystemPasswordChar = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            string correo = textBox1.Text;
            String contraseña = textBox2.Text;
            if(textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Debe llenar todos los campos");
                return;
            }
            using(Model.Session1Entities modelo = new Model.Session1Entities())
            {
                Users users = modelo.Users.FirstOrDefault(u => u.Email == correo && u.Password == contraseña);


                if (users != default(Users)  && users.Active == 1)
                {
                    if(users.RoleID == 1)
                    {
                        Form formA = new FormAdmin { StartPosition = FormStartPosition.CenterScreen };
                        this.Hide();
                        formA.Show();
                        formA.FormClosed += (object s, FormClosedEventArgs e1) =>
                        {
                            this.Show();
                        };
                    }
                    else
                    {
                        Form formB= new FormBienvenida (users,Intento) { StartPosition = FormStartPosition.CenterScreen };
                        this.Hide();
                        formB.Show();
                        formB.FormClosed += (object s, FormClosedEventArgs e1) =>
                        {
                            this.Show();
                        };
                    }
                }
                else if (users == null)
                {
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Su usuario o contraseña son incorrectos";
                    lblMensaje.ForeColor = Color.Orange;
                    Intento++;
                    if (Intento == 3)
                    {
                        this.Enabled = false;
                        for (int i = 10; i >=0; i--)
                        {
                            await Task.Run(() => { Thread.Sleep(1000); });
                            lblMensaje.Text = $"Por favor, espero {i} segundo para intentar de nuevo";
                        }
                        lblMensaje.Visible = false;
                        this.Enabled = true;
                    }
                }
                else if (users.Active == 0)
                {
                    lblMensaje.Visible = true;
                    string usuarioBloquedor = "Sistema";
                    lblMensaje.Text = $"Su usuario ha sido supendido por el { usuarioBloquedor }";
                    lblMensaje.ForeColor = Color.Red;
                    return;


                }
            }
        }
    }
}
