using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAbsence.Models
{
    public class Absence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CodeEtudiant { get; set; }

        [Required]
        public string CodeEnseignant { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string CreneauHoraire { get; set; }

        [Required]
        public bool EstJustifiee { get; set; }

        // Navigation properties
        [ForeignKey("CodeEtudiant")]
        public virtual Etudiant Etudiant { get; set; }

        [ForeignKey("CodeEnseignant")]
        public virtual Enseignant Enseignant { get; set; }
    }
} 