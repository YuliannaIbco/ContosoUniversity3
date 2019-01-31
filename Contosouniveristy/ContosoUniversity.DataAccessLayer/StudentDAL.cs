using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessEntities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace ContosoUniversity.DataAccessLayer
{
    public class StudentDAL
    {
        private static ContosoDbContext1 dbCtx = new ContosoDbContext1();

        #region GUARDAR UN ESTUDIANTE
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
                        #region Entity Framework
                        dbCtx.Students.Add(student);

                        isInserted=dbCtx.SaveChanges()>0;

                        if (isInserted)
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
                }
            }

            return message;
        }
        #endregion

        #region MODIFICAR UN ESTUDIANTE
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
                        #region Entity Framework
                        var entity = dbCtx.Students.Find(student.Id);

                        if (entity != null)
                        {
                            entity.LastName = student.LastName;
                            entity.FirstMidName = student.FirstMidName;

                            dbCtx.Entry(entity).State = EntityState.Modified;

                            isUpdated = dbCtx.SaveChanges() > 0;

                            if (isUpdated)
                            {
                                dbCtxTran.Commit();
                            }
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
                }
            }

            return message;
        }
        #endregion

        #region ELIMINAR UN ESTUDIANTE
        public static string removeStudent(int id)
        {
            //variable para almacenar el mensaje de error en caso de que ocurra alguno
            string message = string.Empty;

            //Variable para almacenar si se inserto correctamente el registro
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
                        #region Entity Framework
                        var objStudent = dbCtx.Students.Where(x => x.Id == id).SingleOrDefault();

                        dbCtx.Students.Remove(objStudent);
                            
                        isRemoved = dbCtx.SaveChanges() > 0;

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
                }
            }

            return message;
        }
        #endregion

        #region DIFERENTES FORMAS DE BUSCAR UN ESTUDIANTE
        public static List<Student> getAllStudents()
        {
            List<Student> students = new List<Student>();

            students = dbCtx.Students.ToList();

            return students;
        }
        public static List<Student> getStudentByLastName(string lastname)
        {
            List<Student> students = new List<Student>();

            students = dbCtx.Students.Where(x => x.LastName == lastname).ToList();

            return students;
        }
        public static List<Student> getStudentByFirstMidName(string fmn)
        {
            List<Student> students = new List<Student>();

            students = dbCtx.Students.Where(x => x.FirstMidName == fmn).ToList();

            return students;
        }
        public static List<Student> getStudentById(int id)
        {
            List<Student> students = new List<Student>();

            students = dbCtx.Students.Where(x => x.Id == id).ToList();

            return students;
        }
        #endregion
    }
}