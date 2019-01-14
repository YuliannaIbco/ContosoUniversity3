using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessEntities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Name es obligatorio")]
        [StringLength(35, ErrorMessage = "La longitud maxima es de 35")]
        public String Name { get; set; }

        [Required(ErrorMessage = "El campo Budget es obligatorio")]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "El campo StartDate es obligatorio")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [ForeignKey("Instructor")]
        [Required(ErrorMessage = "El campo InstructorId es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero entero")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
