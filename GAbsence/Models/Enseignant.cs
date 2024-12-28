using System;
using System.Collections.Generic;
using GAbsence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAbsence.Models
{
    public class Enseignant
    {
        [Required]
        public string CodeEnseignant { get; set; }
        
        [Required]
        public string Nom { get; set; }
        
        [Required]
        public string Prenom { get; set; }
        
        public DateTime DateRecrutement { get; set; }
        
        public string? Adresse { get; set; }
        
        [Required]
        public string Mail { get; set; }
        
        [Required]
        public string Tel { get; set; }

        // Clés étrangères
        [ForeignKey("Departement")]
        [Required]
        public string CodeDepartement { get; set; }
        
        [ForeignKey("Grade")]
        [Required]
        public string CodeGrade { get; set; }

        // Propriétés de navigation
        public virtual Departement? Departement { get; set; }
        public virtual Grade? Grade { get; set; }
    }
} 