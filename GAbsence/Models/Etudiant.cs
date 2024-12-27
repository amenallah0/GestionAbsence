using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Etudiant
    {
        [Key]
        [Required(ErrorMessage = "Le code étudiant est requis")]
        public string CodeEtudiant { get; set; }
        
        [Required(ErrorMessage = "Le nom est requis")]
        public string Nom { get; set; }
        
        [Required(ErrorMessage = "Le prénom est requis")]
        public string Prenom { get; set; }
        
        [Required(ErrorMessage = "La date de naissance est requise")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }
        
        [Required(ErrorMessage = "L'adresse est requise")]
        public string Adresse { get; set; }
        
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress]
        public string Mail { get; set; }
        
        [Required(ErrorMessage = "Le téléphone est requis")]
        public string Tel { get; set; }
        
        [Required(ErrorMessage = "La classe est requise")]
        public string CodeClasse { get; set; }

        // Navigation property
        public virtual Classe Classe { get; set; }
    }
} 