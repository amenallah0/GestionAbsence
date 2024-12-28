using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAbsence.Models
{
    public class FicheAbsence
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public string CreneauHoraire { get; set; }
        
        [Required]
        public string CodeEtudiant { get; set; }
        
        [Required]
        public string CodeEnseignant { get; set; }
        
        [Required]
        public string Matiere { get; set; }
        
        public bool EstJustifiee { get; set; }
        
        public string? Justification { get; set; }
        
        // Navigation properties
        public virtual Etudiant? Etudiant { get; set; }
        public virtual Enseignant? Enseignant { get; set; }
    }
} 