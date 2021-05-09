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
    public partial class FormRecuperacion : Form
    {
        public Users usuario { get; set; }
        public FormRecuperacion(Users u)
        {
            InitializeComponent();
            usuario = u;
        }

        private void FormRecuperacion_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != usuario.Password)
            {
                MessageBox.Show("La contraseña anterior no coincide");
                return;

            }

            if(textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("La contraseña nueva con coincide con la confirmación");
                return;
            }


            using (Session1Entities modelo = new Session1Entities())
            {
                Users users = modelo.Users.FirstOrDefault(u => u.ID == usuario.ID);
                users.Password = textBox2.Text;
                modelo.Entry(users).State = System.Data.Entity.EntityState.Modified;
                if (modelo.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha cambiado al contraseña");
                }
                else
                {
                    MessageBox.Show("NO Se ha cambiado al contraseña");
                }
            }
        }
    }
}
