using Session1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session1
{
    public partial class FormAgregarUsuario : Form
    {
        public FormAgregarUsuario()
        {
            InitializeComponent();
            FillRole();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FillRole()
        {
            using (Model.Session1Entities model = new Model.Session1Entities())
            {
                List<Roles> role = model.Roles.ToList();
                comboBox1.DataSource = role;
                comboBox1.DisplayMember = "Title";
                comboBox1.ValueMember = "ID";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach(Control c in this.Controls)
            {
                if(c is TextBox)
                {
                    if(c.Text == String.Empty)
                    {
                        MessageBox.Show("Debe llenar todos los datos");
                        return;
                    }
                    if(c is DateTimePicker)
                    {
                        if ((c as DateTimePicker).Value.Date == DateTime.Now.Date)
                        {
                            MessageBox.Show("selecione una fecha correcta");
                            return;
                        }
                    }
                }
            }
            using (Session1Entities model = new Session1Entities())
            {
                model.Users.Add(
                    new Users {
                        Active = 1,
                        Birthdate = dateTimePicker1.Value,
                        Email = textBox1.Text,
                        FirstName = textBox2.Text,
                        LastName = textBox3.Text,
                        OfficeID = 3,
                        Password = textBox4.Text,
                        RoleID = 2
                    }
                    );
                if (model.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha guardado correctamente");

                }
                else
                {
                    MessageBox.Show("No se ha guardado correctamente");
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
