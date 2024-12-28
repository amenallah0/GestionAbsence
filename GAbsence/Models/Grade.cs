namespace GAbsence.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Grade
    {
        [Key]
        [Required]
        public string CodeGrade { get; set; }
        [Required]
        public string Libelle { get; set; }
        public virtual ICollection<Enseignant>? Enseignants { get; set; }
    }
} 