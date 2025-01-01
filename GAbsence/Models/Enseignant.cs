using System;
using System.Collections.Generic;
using GAbsence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAbsence.Models
{
    public class Enseignant
    {
        [Key]
        [Required(ErrorMessage = "Le code enseignant est requis")]
        public required string CodeEnseignant { get; set; }
        
        [Required(ErrorMessage = "Le nom est requis")]
        public required string Nom { get; set; }
        
        [Required(ErrorMessage = "Le prénom est requis")]
        public required string Prenom { get; set; }
        
        [Required(ErrorMessage = "La date de recrutement est requise")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de Recrutement")]
        public DateTime DateRecrutement { get; set; }
        
        [Required(ErrorMessage = "L'adresse est requise")]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }
        
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [Display(Name = "Email")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Le téléphone est requis")]
        [Phone(ErrorMessage = "Format de téléphone invalide")]
        [Display(Name = "Téléphone")]
        public required string Tel { get; set; }

        // Clés étrangères
        [Required(ErrorMessage = "Le département est requis")]
        [Display(Name = "Département")]
        public required string CodeDepartement { get; set; }
        
        [Required(ErrorMessage = "Le grade est requis")]
        [Display(Name = "Grade")]
        public required string CodeGrade { get; set; }

        // Propriétés de navigation
        public virtual Departement? Departement { get; set; }
        public virtual Grade? Grade { get; set; }
        public virtual ICollection<Absence>? Absences { get; set; } = new HashSet<Absence>();
        public virtual ICollection<Matiere>? Matieres { get; set; }

        [NotMapped]
        public string NomComplet => $"{Nom} {Prenom}";

        public Enseignant()
        {
            Absences = new HashSet<Absence>();
            Matieres = new HashSet<Matiere>();
        }
    }
} 