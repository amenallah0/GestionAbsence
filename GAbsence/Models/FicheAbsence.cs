using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class FicheAbsence
    {
        [Key]
        public required string CodeFicheAbsence { get; set; }
        
        [Required(ErrorMessage = "La date est requise")]
        [DataType(DataType.Date)]
        public DateTime DateJour { get; set; }
        
        [Required(ErrorMessage = "Le code enseignant est requis")]
        public required string CodeEnseignant { get; set; }

        // Navigation properties
        public virtual Enseignant? Enseignant { get; set; }
        public virtual ICollection<FicheAbsenceSeance>? FicheAbsenceSeances { get; set; }
    }
} 