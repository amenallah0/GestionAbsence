using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Matiere
    {
        [Key]
        public string CodeMatiere { get; set; }
        
        [Required]
        public string Libelle { get; set; }
        
        [Required]
        public int NbreHeures { get; set; }
        
        [Required]
        public int Coefficient { get; set; }
    }
} 