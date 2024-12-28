using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class FicheAbsenceSeance
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int FicheAbsenceId { get; set; }
        
        [Required]
        public string CodeSeance { get; set; }

        // Navigation properties
        public virtual FicheAbsence FicheAbsence { get; set; }
        public virtual Seance Seance { get; set; }
        public virtual ICollection<LigneFicheAbsence> LignesFicheAbsence { get; set; }
    }
} 