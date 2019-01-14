using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessEntities
{
    public class Student
    {
        [Key]//Primary Key
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo LastName es obligatorio")]//Obligatorio
        [StringLength(35, ErrorMessage = "La longitud máxima es de 35 caracteres")]//Longitud de la cadena
        public String LastName { get; set; }

        [Required(ErrorMessage = "El campo FirstMidName es obligatorio")]
        [StringLength(50, ErrorMessage = "La longitud maxima es de 50 caracteres")]
        public String FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo EnrollmentDate es obligatorio")]
        [DataType(DataType.DateTime)]//indicar tipo de fecha
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
