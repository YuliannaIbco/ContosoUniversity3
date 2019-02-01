using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using businessEntities;

namespace ContosoUniversity.DesktopApp
{
    public partial class frmStudent : Form
    {
        public frmStudent()
        {
            InitializeComponent();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            gvStudent.DataSource = ContosoUniversity.BusinessLogicLayer2.StudentBLL.getAllStudents();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                DataGridViewRow row = this.gvStudent.Rows[e.RowIndex];

                txtId.Text = row.Cells["Id"].Value.ToString();
                txtln.Text = row.Cells["LastName"].Value.ToString();
                txtfmn.Text = row.Cells["FirstMidName"].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            string lastname = txtln.Text.Trim().ToString();
            string name = txtfmn.Text.Trim().ToString();

            student.LastName = lastname;
            student.FirstMidName = name;

            String message = ContosoUniversity.BusinessLogicLayer2.StudentBLL.insertStudent(student);

            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Registrado");
                gvStudent.DataSource = ContosoUniversity.BusinessLogicLayer2.StudentBLL.getAllStudents();
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void btnSea_Click(object sender, EventArgs e)
        {
            List<Student> students = new List<Student>();

            //Declaro la variable para almacenar el filtro de busqueda
            string filter = string.Empty;
            //declaro una variable para almacenar la bandera
            string flag = string.Empty;

            if (string.IsNullOrEmpty(txtId.Text.Trim().ToString()))
            {
                if (string.IsNullOrEmpty(txtln.Text.Trim().ToString()))
                {
                    filter = txtfmn.Text.Trim().ToString();
                    flag = "FirstMidName";
                }
                else
                {
                    filter = txtln.Text.Trim().ToString();
                    flag = "LastName";
                }
            }
            else
            {
                filter = txtId.Text.Trim().ToString();
                flag = "StudentId";
            }

            //Crear el puente entre el BLL y la UI
            students = ContosoUniversity.BusinessLogicLayer2.StudentBLL.getStudentByFilter(filter, flag);

            //Mostrar los datos obtenidos en los textbox
            foreach (var i in students)
            {
                txtId.Text = i.Id.ToString();
                txtln.Text = i.LastName;
                txtfmn.Text = i.FirstMidName;
            }

            gvStudent.DataSource = students;
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            //Crea la instancia del objeto a trabajar
            Student student = new Student();

            //Declaramos variables y las igualamos a las cajas de texto
            string nombre = txtfmn.Text.Trim().ToString();
            string apellido = txtln.Text.Trim().ToString();
            int id = Convert.ToInt32(txtId.Text.Trim().ToString());

            //Asignamos variables
            student.FirstMidName = nombre;
            student.LastName = apellido;
            student.Id = id;

            //Puente entre el BusinessLogicLayer y la interfaz Grafica
            String message = ContosoUniversity.BusinessLogicLayer2.StudentBLL.updateStudent(student);

            //Es para validar si ocurrio algun error
            if (string.IsNullOrEmpty(message))
            {
                //Si no hubo errores, muestra un mensaje de confirmacion
                MessageBox.Show("El registro ha sido editado correctamente");

                //Precargado
                gvStudent.DataSource = ContosoUniversity.BusinessLogicLayer2.StudentBLL.getAllStudents();
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //Crea la instancia del objeto a trabajar
            Student student = new Student();

            //Declaramos variables y las igualamos a las cajas de texto
            string nombre = txtfmn.Text.Trim().ToString();
            string apellido = txtln.Text.Trim().ToString();
            int id = Convert.ToInt32(txtId.Text.Trim().ToString());

            //Asignamos variables
            student.FirstMidName = nombre;
            student.LastName = apellido;
            student.Id = id;

            //Puente entre el BusinessLogicLayer y la interfaz Grafica
            String message = ContosoUniversity.BusinessLogicLayer2.StudentBLL.removeStudent(id);

            //Es para validar si ocurrio algun error
            if (string.IsNullOrEmpty(message))
            {
                //Si no hubo errores, muestra un mensaje de confirmacion
                MessageBox.Show("El registro ha sido eliminado correctamente");
                
                //Precargado
                gvStudent.DataSource=ContosoUniversity.BusinessLogicLayer2.StudentBLL.getAllStudents();
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}