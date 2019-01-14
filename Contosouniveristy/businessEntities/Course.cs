using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessEntities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Title es obligatorio")]
        [StringLength(50, ErrorMessage = "La longitud maxima es de 50")]
        public String Title { get; set; }

        [Required(ErrorMessage = "El campo Credits es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero entero")]
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }//es el lugar donde esta siendo llamada la llave foranea

        [ForeignKey("Department")]
        [Required(ErrorMessage = "El campo DepartmentId es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero entero")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
