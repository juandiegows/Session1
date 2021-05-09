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
using Session1.View;

namespace Session1
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Session1Entities model = new Session1Entities())
            {
                int.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString(), out int id);
                Users users = model.Users.FirstOrDefault(u => u.ID == id);
                Form formA = new FormCambiarRol(users) { StartPosition = FormStartPosition.CenterScreen };
                this.Enabled = false;
                formA.Show();
                formA.FormClosed += (object s, FormClosedEventArgs e1) =>
                {
                    this.Enabled = true;
                    llenarTabla();
                };
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form formA = new FormAgregarUsuario { StartPosition = FormStartPosition.CenterScreen };
            this.Enabled = false;
            formA.Show();
            formA.FormClosed += (object s, FormClosedEventArgs e1) =>
            {
                this.Enabled = true;
                llenarTabla();
            };
        }

        private void llenarCombo()
        {
            using(Model.Session1Entities modelo = new Model.Session1Entities())
            {
                List<Offices> offices = modelo.Offices.ToList();
                offices.Insert(0, new Offices { Title = "Todas", ID = 0 });
                comboBox1.DataSource = offices;
                comboBox1.DisplayMember = "Title";
                comboBox1.ValueMember = "ID";
            }
           
        }

        private void llenarTabla()
        {
            using (Model.Session1Entities modelo = new Model.Session1Entities())
            {
                int.TryParse(comboBox1.SelectedValue.ToString(), out int id);
                List<UsuarioView> usuariosList = modelo.Users.Where(u=>u.OfficeID == id || id ==0).ToList().Select(u=> new UsuarioView {
                    ID = u.ID,
                    Nombre = u.FirstName,
                    Apellido = u.LastName,
                    Correo = u.Email,
                    Edad = DateTime.Now.Year - u.Birthdate.Value.Year,
                    Rol = u.Roles.Title,
                    Oficina = u.Offices.Title,
                    Activo =(int) u.Active

                }).ToList();
                dataGridView1.DataSource = usuariosList;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[7].Visible = false;
            }
        }
        private void FormAdmin_Load(object sender, EventArgs e)
        {
            llenarCombo();
            llenarTabla();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarTabla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Session1Entities model = new Session1Entities())
            {
                int.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString(), out int id);
                Users users = model.Users.FirstOrDefault(u => u.ID == id);
                users.Active = 1;
                model.Entry(users).State = System.Data.Entity.EntityState.Modified;
                if (model.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha restablecido la cuenta");
                    llenarTabla();
                }
                else
                {
                    MessageBox.Show("No Se restablecido la cuenta");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Session1Entities model = new Session1Entities())
            {
                int.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString(), out int id);
                Users users = model.Users.FirstOrDefault(u => u.ID == id);
                users.Active = 0;
                model.Entry(users).State = System.Data.Entity.EntityState.Modified;
                if (model.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha suspendido la cuenta");
                    llenarTabla();
                }
                else
                {
                    MessageBox.Show("No Se suspendido la cuenta");
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
          
         
           
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
