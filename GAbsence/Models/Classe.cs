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
        
        public virtual ICollection<Etudiant>? Etudiants { get; set; }
    }
} 