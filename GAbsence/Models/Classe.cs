using System.Collections.Generic;
using GAbsence.Models;
using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Classe
    {
        [Key]
        [Required(ErrorMessage = "Le code de la classe est requis")]
        [Display(Name = "Code de la classe")]
        public string CodeClasse { get; set; }
        
        [Required(ErrorMessage = "Le nom de la classe est requis")]
        [Display(Name = "Nom de la classe")]
        public string NomClasse { get; set; }

        [Required(ErrorMessage = "Le niveau est requis")]
        [Range(1, 5, ErrorMessage = "Le niveau doit être entre 1 et 5")]
        [Display(Name = "Niveau")]
        public int Niveau { get; set; }

        [Required(ErrorMessage = "La filière est requise")]
        [Display(Name = "Filière")]
        public string CodeFiliere { get; set; }

        // Propriétés de navigation
        public virtual ICollection<Etudiant>? Etudiants { get; set; }
        public virtual Filiere? Filiere { get; set; }

        public Classe()
        {
            Etudiants = new HashSet<Etudiant>();
        }
    }
} 