using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessEntities
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Course")]//indica la llave foranea y en las comillas va la tabla de origen
        [Required(ErrorMessage = "El campo CourseId es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero entero")]
        public int CourseId { get; set; }
        public Course Course { get; set; }//Agregar el objeto de donde viene la llave foranea

        [ForeignKey("Student")]
        [Required(ErrorMessage = "El campo StudentId es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero entero")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required(ErrorMessage = "El campo Grade es obligatorio")]
        [StringLength(5, ErrorMessage = "La longitud maxima es de 5")]
        public String Grade { get; set; }
    }
}
