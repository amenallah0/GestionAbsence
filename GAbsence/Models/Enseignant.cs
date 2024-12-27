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
        public string CodeEnseignant { get; set; }
        
        public string Nom { get; set; }
        
        public string Prenom { get; set; }
        
        public DateTime DateRecrutement { get; set; }
        
        public string Adresse { get; set; }
        
        public string Mail { get; set; }
        
        public string Tel { get; set; }

        // Clés étrangères
        [ForeignKey("Departement")]
        public string CodeDepartement { get; set; }
        
        [ForeignKey("Grade")]
        public string CodeGrade { get; set; }

        // Propriétés de navigation
        public virtual Departement? Departement { get; set; }
        public virtual Grade? Grade { get; set; }
    }
} 