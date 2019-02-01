using businessEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace ContosoUniversity.DataAccessLayer
{
    public class StudentDAL
    {
        private static ContosoDbContext1 dbCtx = new ContosoDbContext1();

        
        public static string insertStudent(Student student)
        {
            //variable para almacenar el mensaje de error en caso de que ocurra alguno
            string message = string.Empty;

            //Variable para almacenar si se inserto correctamente el registro
            bool isInserted = false;

            //Se utiliza un espacio de nombres para utilizar las transacciones
            using (var dbCtxTran = dbCtx.Database.BeginTransaction())
            {
                try
                {
                    //se verifica si la base de dstos existe
                    bool isDataBaseExist = Database.Exists(dbCtx.Database.Connection);

                    //Si existe la base de datos se procede a insertar el registro
                    if (isDataBaseExist)
                    {
                        #region Alternativa #1 - Funcion para insertar en la base de datos por medio de Entity Framework

                        //Se añade la entidad al contexto
                        dbCtx.Students.Add(student);

                        //Se guardan los cambios en el contexto y se verifica si se efectuo de manera correcta
                        isInserted = dbCtx.SaveChanges() > 0;

                        //Si se realizo correctamente la insercion se procede a ejecutar la transaccion
                        if (isInserted)
                        {
                            //Se hace commit a la transaccion
                            dbCtxTran.Commit();
                        }

                        #endregion
                    }
                }//fin de try
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ",
                        fullErrorMessage);
                    message = exceptionMessage + "\n" + ex.EntityValidationErrors;

                    dbCtxTran.Rollback();
                }//Fin de DbEntityValidationException
                catch (DbUpdateConcurrencyException ex)
                {
                    var entityObj = ex.Entries.Single().GetDatabaseValues();

                    if (entityObj == null)

                        message = "The entity being updated is already deleted by another user";
                    else
                        message = "The entity being updated has already been updated by another user";

                    dbCtxTran.Rollback();
                } // Fin de DbUpdateConcurrencyException

                catch (Exception ex)
                {
                    message = ex.Message;

                    dbCtxTran.Rollback();
                } // fin de Exception
            }// fin transaccion

            //Retorna el mensaje de error, en caso de ocurrir alguno de lo contrario regress vcio
            return message;
        }// Fin metodo
        

        #region ACTUALIZAR ESTUDIANTE
        public static string updateStudent(Student student)
        {
            //variable para almacenar el mensaje de error en caso de que ocurra alguno
            string message = string.Empty;

            //Variable para almacenar si se inserto correctamente el registro
            bool isUpdated = false;

            //Se utiliza un espacio de nombres para utilizar las transacciones
            using (var dbCtxTran = dbCtx.Database.BeginTransaction())
            {
                try
                {
                    //se verifica si la base de dstos existe
                    bool isDataBaseExist = Database.Exists(dbCtx.Database.Connection);

                    //Si existe la base de datos se procede a insertar el registro
                    if (isDataBaseExist)
                    {
                        #region Alternativa #1 - Actualizando con Entity Framework
                        //buscamos el objeto a actualizar
                        var entity = dbCtx.Students.Find(student.Id);

                        //Validad que el objeto exsita
                        if (entity != null)
                        {
                            //Damos los valores a modificar
                            entity.LastName = student.LastName;
                            entity.FirstMidName = student.FirstMidName;

                            //Se añade la entidad al contexto
                            dbCtx.Entry(entity).State = EntityState.Modified;

                            //Se guardan los cambios en el contexto y se verifica si se efectuo de manera correcta
                            isUpdated = dbCtx.SaveChanges() > 0;

                            if (isUpdated)
                            {
                                dbCtxTran.Commit();
                            }//Fin de la validacion isUpdated == true
                        }//Fin de la validacion entity != null
                        #endregion
                    }//fin de la validacion de DataBaseExist
                }//fin de try
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ",
                        fullErrorMessage);
                    message = exceptionMessage + "\n" + ex.EntityValidationErrors;

                    dbCtxTran.Rollback();
                }//Fin de DbEntityValidationException
                catch (DbUpdateConcurrencyException ex)
                {
                    var entityObj = ex.Entries.Single().GetDatabaseValues();

                    if (entityObj == null)

                        message = "The entity being updated is already deleted by another user";
                    else
                        message = "The entity being updated has already been updated by another user";

                    dbCtxTran.Rollback();
                } // Fin de DbUpdateConcurrencyException

                catch (Exception ex)
                {
                    message = ex.Message;

                    dbCtxTran.Rollback();
                } // fin de Exception
            }// fin transaccion

            //Retorna el mensaje de error, en caso de ocurrir alguno de lo contrario regress vcio
            return message;
        }
        #endregion

        #region ELIMINAR ESTUDIANTE
        public static string removeStudent(int Id)
        {
            //variable para almacenar el mensaje de error en caso de que ocurra alguno
            string message = string.Empty;

            //Variable para almacenar el status del metodo
            bool isRemoved = false;

            //Se utiliza un espacio de nombres para utilizar las transacciones
            using (var dbCtxTran = dbCtx.Database.BeginTransaction())
            {
                try
                {
                    //se verifica si la base de dstos existe
                    bool isDataBaseExist = Database.Exists(dbCtx.Database.Connection);

                    //Si existe la base de datos se procede a insertar el registro
                    if (isDataBaseExist)
                    {
                        #region Alternativa #1 - Eliminando con Entity Framework
                        //Consultar para obtener el objeto a eliminar
                        //var objStudent = dbCtx.Students.Find(Id);
                        var objStudent = dbCtx.Students.Where(x => x.Id == Id).SingleOrDefault();//Puede ser esta también para que elimine

                        //Consulta para eliminar el objeto
                        dbCtx.Students.Remove(objStudent);

                        //Guardar el status del borrado
                        isRemoved = dbCtx.SaveChanges() > 0;

                        //Si se elimino correctamente se hace el commit
                        if (isRemoved)
                        {
                            dbCtxTran.Commit();
                        }
                        #endregion
                    }
                }//fin de try
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ",
                        fullErrorMessage);
                    message = exceptionMessage + "\n" + ex.EntityValidationErrors;

                    dbCtxTran.Rollback();
                }//Fin de DbEntityValidationException
                catch (DbUpdateConcurrencyException ex)
                {
                    var entityObj = ex.Entries.Single().GetDatabaseValues();

                    if (entityObj == null)

                        message = "The entity being updated is already deleted by another user";
                    else
                        message = "The entity being updated has already been updated by another user";

                    dbCtxTran.Rollback();
                } // Fin de DbUpdateConcurrencyException

                catch (Exception ex)
                {
                    message = ex.Message;

                    dbCtxTran.Rollback();
                } // fin de Exception
            }// fin transaccion

            //Retorna el mensaje de error, en caso de ocurrir alguno de lo contrario regress vcio
            return message;
        }
        #endregion

        #region FORMAS DE BUSCAR UN ESTUDIANTE

        #region BUSCAR ESTUDIANTES (TODOS)
        /// <summary>
        /// Metodo para obtener todos los estudiantes registrados
        /// en pocas palabras un SELECT * FROM
        /// </summary>
        /// <returns></returns>
        public static List<Student> getAllStudent()
        {
            List<Student> students = new List<Student>();//se crea la instacia de la lista de estudiantes

            //SELECT * FROM Students
            students = dbCtx.Students.ToList();

            return students;//retorna lista de estudiantes
        }
        #endregion

        #region BUSCAR ESTUDIANTE 2 (POR APELLIDO)
        public static List<Student> getStudentsByLastName(string lastname)
        {
            List<Student> students = new List<Student>();//se crea la instacia de la lista de estudiantes

            //SELECT * FROM Students WHERE LastName='___'
            students = dbCtx.Students.Where(x => x.LastName == lastname).ToList();

            return students;
        }
        #endregion

        #region BUSCAR ESTUDIANTE 3 (POR ID)
        public static List<Student> getStudentByID(int ID)
        {
            List<Student> students = new List<Student>();

            //SELECT * FROM .... WHERE ID=?
            students = dbCtx.Students.Where(x => x.Id == ID).ToList();

            return students;
        }
        #endregion

        #region BUSCAR ESTUDIANTE 4 (POR NOMBRE COMPLETO)
        public static List<Student> getStudentByFirstMidName(string firstmidname)
        {
            List<Student> students = new List<Student>();

            //SELECT * FROM .... WHERE firstmidname = '_____'
            students = dbCtx.Students.Where(x => x.FirstMidName == firstmidname).ToList();

            return students;
        }
        #endregion

        #endregion
    }
}