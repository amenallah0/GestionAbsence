using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Matiere
    {
        [Key]
        [Required(ErrorMessage = "Le code matière est obligatoire")]
        public string CodeMatiere { get; set; }
        
        [Required(ErrorMessage = "Le libellé est obligatoire")]
        [Display(Name = "Nom de la matière")]
        public string Libelle { get; set; }
        
        [Required]
        [Display(Name = "Nombre d'heures")]
        public int NbreHeures { get; set; }
        
        [Required]
        public int Coefficient { get; set; }
        
        // Navigation properties
        public virtual ICollection<Absence>? Absences { get; set; }
        
        // Ajout de la relation avec les enseignants
        public virtual ICollection<Enseignant>? Enseignants { get; set; }
    }
} 