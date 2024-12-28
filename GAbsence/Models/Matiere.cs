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
        public int NbreHeures { get; set; }
        
        [Required]
        public int Coefficient { get; set; }
        
        // Navigation properties
        public virtual ICollection<Absence>? Absences { get; set; }
    }
} 