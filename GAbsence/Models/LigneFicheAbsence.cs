using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class LigneFicheAbsence
    {
        [Required]
        public required string CodeFicheAbsence { get; set; }
        
        [Required]
        public required string CodeEtudiant { get; set; }

        // Navigation properties
        public virtual FicheAbsenceSeance? FicheAbsenceSeance { get; set; }
        public virtual Etudiant? Etudiant { get; set; }
    }
} 