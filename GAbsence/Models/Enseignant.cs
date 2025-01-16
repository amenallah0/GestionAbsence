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
        [Display(Name = "Code Enseignant")]
        public string CodeEnseignant { get; set; }
        
        [Required(ErrorMessage = "Le nom est requis")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }
        
        [Required(ErrorMessage = "Le prénom est requis")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        
        [Required(ErrorMessage = "La date de recrutement est requise")]
        [Display(Name = "Date de recrutement")]
        [DataType(DataType.Date)]
        public DateTime DateRecrutement { get; set; }
        
        [Required(ErrorMessage = "L'adresse est requise")]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }
        
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Le téléphone est requis")]
        [Display(Name = "Téléphone")]
        public string Tel { get; set; }

        // Clés étrangères
        [Required(ErrorMessage = "Le département est requis")]
        [Display(Name = "Département")]
        public string CodeDepartement { get; set; }
        
        [Required(ErrorMessage = "Le grade est requis")]
        [Display(Name = "Grade")]
        public string CodeGrade { get; set; }

        // Ajout de la relation avec ApplicationUser
        public string? UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

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