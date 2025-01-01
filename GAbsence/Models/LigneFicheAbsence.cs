using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAbsence.Models
{
    public class LigneFicheAbsence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CodeFiche { get; set; }

        [Required]
        public string CodeEtudiant { get; set; }

        public bool EstAbsent { get; set; }

        // Navigation properties
        public virtual FicheAbsence? FicheAbsence { get; set; }
        public virtual Etudiant? Etudiant { get; set; }
    }
} 