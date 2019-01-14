using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessEntities
{
    public class OfficeAssignment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Instructor")]
        [Required(ErrorMessage = "El campo InstructorId es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero entero")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        [Required(ErrorMessage = "El campo Location es obligatorio")]
        [StringLength(40, ErrorMessage = "La longitud maxima es de 40 caracteres")]
        public String Location { get; set; }
    }
}
