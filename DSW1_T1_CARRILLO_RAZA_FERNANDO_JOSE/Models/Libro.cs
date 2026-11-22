using System.ComponentModel.DataAnnotations;

namespace DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del libro es obligatorio")]
        [StringLength(120)]
        public string NombreLibro { get; set; } = string.Empty;

        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(100)]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "La editorial es obligatoria")]
        [StringLength(100)]
        public string NombreEditorial { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ISDN es obligatorio")]
        [StringLength(20)]
        public string ISDN { get; set; } = string.Empty;
    }
}