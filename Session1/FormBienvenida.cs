using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Session1.Model;

namespace Session1
{
    public partial class FormBienvenida : Form
    {
        public Users Usuario {get; set; }
        public FormBienvenida(Users users, int intento = 0)
        {
            InitializeComponent();
            Usuario = users;
            label2.Text = $"Hola {Usuario.FirstName } { Usuario.LastName} Bienvenido al sistema de autentificación de amonic";
            label1.Text = $"numero de intentos fallido : {intento}";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formB = new FormRecuperacion(Usuario) { StartPosition = FormStartPosition.CenterScreen };
            this.Enabled = false;
            formB.Show();
            formB.FormClosed += (object s, FormClosedEventArgs e1) =>
            {
                this.Enabled = true;
            };
         

        }

        private void FormBienvenida_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
