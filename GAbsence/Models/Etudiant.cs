using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class Etudiant
    {
        [Key]
        public required string CodeEtudiant { get; set; }
        
        [Required(ErrorMessage = "Le nom est requis")]
        public required string Nom { get; set; }
        
        [Required(ErrorMessage = "Le prénom est requis")]
        public required string Prenom { get; set; }
        
        [Required(ErrorMessage = "La date de naissance est requise")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }
        
        [Required(ErrorMessage = "Le code classe est requis")]
        public required string CodeClasse { get; set; }
        
        [Required(ErrorMessage = "L'adresse est requise")]
        public required string Adresse { get; set; }
        
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress]
        public required string Mail { get; set; }
        
        [Required(ErrorMessage = "Le téléphone est requis")]
        [Phone]
        public required string Tel { get; set; }

        // Navigation properties
        public virtual Classe? Classe { get; set; }
        public virtual ICollection<LigneFicheAbsence>? LignesFicheAbsence { get; set; }
    }
} 