using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class FicheAbsenceSeance
    {
        [Required]
        public required string CodeFicheAbsence { get; set; }
        
        [Required]
        public required string CodeSeance { get; set; }

        // Navigation properties
        public virtual FicheAbsence? FicheAbsence { get; set; }
        public virtual Seance? Seance { get; set; }
        public virtual ICollection<LigneFicheAbsence>? LignesFicheAbsence { get; set; }
    }
} 