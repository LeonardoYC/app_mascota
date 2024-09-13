using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace app_mascota.Models
{
    [Table("t_mascota")]
    public class Mascota
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Nombre { get; set; }
        public string? Raza { get; set; }
        public string? Color { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Range(typeof(DateTime), "1900-01-01", "2024-12-31", ErrorMessage = "La fecha de nacimiento debe ser entre 1900 y 2024.")]
        public DateTime FechaNacimiento { get; set; }
        
    }
}