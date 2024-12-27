using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Etudiant
    {
        [Key]
        [Required(ErrorMessage = "Le code étudiant est requis")]
        [Display(Name = "Code Étudiant")]
        public string CodeEtudiant { get; set; }
        
        [Required(ErrorMessage = "Le nom est requis")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }
        
        [Required(ErrorMessage = "Le prénom est requis")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        
        [Required(ErrorMessage = "La date de naissance est requise")]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }
        
        [Required(ErrorMessage = "L'adresse est requise")]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }
        
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
        [Display(Name = "Email")]
        public string Mail { get; set; }
        
        [Required(ErrorMessage = "Le téléphone est requis")]
        [Display(Name = "Téléphone")]
        public string Tel { get; set; }
        
        [Required(ErrorMessage = "La classe est requise")]
        [Display(Name = "Classe")]
        public string CodeClasse { get; set; }

        // Navigation property
        public virtual Classe? Classe { get; set; }
    }
} 