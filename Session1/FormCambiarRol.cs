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
    public partial class FormCambiarRol : Form
    {
        public Users usuario { get; set; }
        public FormCambiarRol(Users u)
        {
            InitializeComponent();
            usuario = u;
        }

        private void llenarRol()
        {
            using (Model.Session1Entities modelo = new Model.Session1Entities())
            {
                List<Roles> rol = modelo.Roles.ToList();
                comboBox1.DataSource = rol;
                comboBox1.DisplayMember = "Title";
                comboBox1.ValueMember = "ID";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (Session1Entities modelo = new Session1Entities())
            {
                Users users = modelo.Users.FirstOrDefault(u => u.ID == usuario.ID);
                users.RoleID = (int)comboBox1.SelectedValue;
                modelo.Entry(users).State = System.Data.Entity.EntityState.Modified;
                if (modelo.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha actualizado el rol");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No Se ha actualizado el rol");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCambiarRol_Load(object sender, EventArgs e)
        {
            llenarRol();
        }
    }
}
