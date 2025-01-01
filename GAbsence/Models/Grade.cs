namespace GAbsence.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Grade
    {
        [Key]
        [Required(ErrorMessage = "Le code du grade est requis")]
        [Display(Name = "Code Grade")]
        public string CodeGrade { get; set; } = null!;

        [Required(ErrorMessage = "Le libellé du grade est requis")]
        [Display(Name = "Libellé")]
        public string Libelle { get; set; } = null!;

        // Navigation property
        public virtual ICollection<Enseignant> Enseignants { get; set; } = new HashSet<Enseignant>();

        // Propriété calculée pour la compatibilité
        [NotMapped]
        public string NomGrade => Libelle;
    }
} 