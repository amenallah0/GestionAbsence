using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Filiere
    {
        [Key]
        [Required(ErrorMessage = "Le code de la filière est requis")]
        [Display(Name = "Code Filière")]
        public string CodeFiliere { get; set; }

        [Required(ErrorMessage = "Le nom de la filière est requis")]
        [Display(Name = "Nom de la Filière")]
        public string NomFiliere { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        // Propriétés de navigation
        public virtual ICollection<Classe> Classes { get; set; }

        public Filiere()
        {
            Classes = new HashSet<Classe>();
        }
    }
} 