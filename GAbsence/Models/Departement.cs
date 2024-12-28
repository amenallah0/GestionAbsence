using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Departement
    {
        [Key]
        [Required(ErrorMessage = "Le code du département est requis")]
        [Display(Name = "Code Département")]
        public string CodeDepartement { get; set; }

        [Required(ErrorMessage = "Le nom du département est requis")]
        [Display(Name = "Nom du Département")]
        public string NomDepartement { get; set; }

        [Display(Name = "Date de Création")]
        [DataType(DataType.Date)]
        public DateTime? DateCreation { get; set; }

        // Propriétés de navigation
        public virtual ICollection<Classe> Classes { get; set; }
        public virtual ICollection<Enseignant> Enseignants { get; set; }

        public Departement()
        {
            Classes = new HashSet<Classe>();
            Enseignants = new HashSet<Enseignant>();
        }
    }
} 