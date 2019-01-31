using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessEntities;
using ContosoUniversity.DataAccessLayer;
using System.Data;

namespace ContosoUniversity.BusinessLogicLayer2
{
    public class StudentBLL
    {
        public static string insertStudent(Student student)
        {
            //variable para almacenar el mensaje de error en caso de que ocurra alguno
            string message = string.Empty;

            if (string.IsNullOrEmpty(student.LastName))
            {
                message = "Campo vacío, favor de completarlo";
            }
            else
            {
                if (string.IsNullOrEmpty(student.FirstMidName))
                {
                    message = "Campo vacío, favor de completarlo";
                }
                else
                {
                    student.EnrollmentDate = DateTime.Now;

                    message = ContosoUniversity.DataAccessLayer.StudentDAL.insertStudent(student);
                }
            }

            return message;
        }

        public static string updateStudent(Student student)
        {
            //variable para almacenar el mensaje de error en caso de que ocurra alguno
            string message = string.Empty;

            if (string.IsNullOrEmpty(student.LastName))
            {
                message = "Campo vacío, favor de completarlo";
            }
            else
            {
                if (string.IsNullOrEmpty(student.FirstMidName))
                {
                    message = "Campo vacío, favor de completarlo";
                }
                else
                {
                    student.EnrollmentDate = DateTime.Now;

                    message = ContosoUniversity.DataAccessLayer.StudentDAL.updateStudent(student);
                }
            }

            return message;
        }

        public static string removeStudent(int id)
        {
            //variable para almacenar el mensaje de error en caso de que ocurra alguno
            string message = string.Empty;

            if (id > 0)
            {
                return ContosoUniversity.DataAccessLayer.StudentDAL.removeStudent(id);
            }
            else
            {
                return message;
            }
        }

        public static List<Student> getAllStudents()
        {
            List<Student> students = new List<Student>();

            students = ContosoUniversity.DataAccessLayer.StudentDAL.getAllStudents();

            return students;
        }
        public static List<Student> getStudentByLastName(string lastname)
        {
            List<Student> students = new List<Student>();

            students = ContosoUniversity.DataAccessLayer.StudentDAL.getStudentByLastName(lastname);

            return students;
        }
        public static List<Student> getStudentByFirstMidName(string fmn)
        {
            List<Student> students = new List<Student>();

            students = ContosoUniversity.DataAccessLayer.StudentDAL.getStudentByFirstMidName(fmn);

            return students;
        }
        public static List<Student> getStudentById(int id)
        {
            List<Student> students = new List<Student>();

            students = ContosoUniversity.DataAccessLayer.StudentDAL.getStudentById(id);

            return students;
        }
        public static List<Student> getStudentByFilter(string filter,string flag)
        {
            List<Student> students = new List<Student>();

            switch (flag)
            {
                case "FirstMidName":
                    students = ContosoUniversity.DataAccessLayer.StudentDAL.getStudentByFirstMidName(filter);
                    break;
                case "LastName":
                    students = ContosoUniversity.DataAccessLayer.StudentDAL.getStudentByLastName(filter);
                    break;
                case "Id":
                    int id = Convert.ToInt32(filter);
                    students = ContosoUniversity.DataAccessLayer.StudentDAL.getStudentById(id);
                    break;
            }
            return students;
        }
    }
}