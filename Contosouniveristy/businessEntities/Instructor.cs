using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessEntities
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo LastName es obligatorio")]
        [StringLength(35, ErrorMessage = "La longitud maxima es de 35")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "El campo FirstMidName es obligatorio")]
        [StringLength(50, ErrorMessage = "La longitud maxima es de 50")]
        public String FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo HireDate es obligatorio")]
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }

        //public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<OfficeAssignment> OfficeAssignments { get; set; }
    }
}
