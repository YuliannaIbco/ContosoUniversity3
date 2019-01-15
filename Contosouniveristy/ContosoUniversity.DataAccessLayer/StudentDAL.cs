using businessEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccessLayer
{
    public class StudentDAL
    {
        //se crea una instancia de la clasa Db context para accederr a la BD 
        public static ContosoDbContext1 dbCtx = new ContosoDbContext1();

       public static string insertStudent(Student student)
        {
            //variable para almacenar el mensaje de error 
            //en caso de que ocurra alguno 
            string message = string.Empty;

            try
            {
                #region Funcion para insertar en la base de datos por medio de LINQ
                
                //Esta linea es como si fuera un INSERT INTO
                dbCtx.Students.Add(student);

                //Refrescar cambios los cambios 
                dbCtx.SaveChanges();
                #endregion

                #region Funcion para insertar en la base de datos por medio de MySqlCommand(query)
                string connectionString= 
                   
                    ConfigurationManager.ConnectionStrings["ContosoDbContext"].ConnectionString;

                using (MySqlConnection mySqlConn=new MySqlConnection(connectionString))
                {
                    //poner letras en minuscula los campos
                    string Query = "INSERT INTO Student(LastName,FirstMidName,EnrollmentDate)" + "VALUES(@lastname,@firstmidname,@enrollementdate);";


                    using(MySqlCommand mySqlCmd=new MySqlCommand(Query, mySqlConn)){

                        mySqlCmd.Parameters.AddWithValue("@lastname", student.LastName);
                        mySqlCmd.Parameters.AddWithValue("@firstmidname", student.FirstMidName);
                        mySqlCmd.Parameters.AddWithValue("@enrollementdate", student.EnrollmentDate);
                       
                    }

                }


            }
            #endregion 
            catch (Exception ex)
            {

                message = ex.Message;
            }
            //Funcion para insertar en la base de datos 
            return message;
        }
    }
}
