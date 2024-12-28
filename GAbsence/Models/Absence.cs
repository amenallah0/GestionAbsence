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
    [Display(Name = "Code Étudiant")]
    public string CodeEtudiant { get; set; }

    [Required]
    [Display(Name = "Code Enseignant")]
    public string CodeEnseignant { get; set; }

    [Required]
    [Display(Name = "Code Matière")]
    public string CodeMatiere { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    [Display(Name = "Créneau Horaire")]
    public string CreneauHoraire { get; set; }

    [Display(Name = "Justifiée")]
    public bool EstJustifiee { get; set; }

    public string? Justification { get; set; }

    // Navigation properties avec configuration explicite des clés étrangères
    [ForeignKey("CodeEtudiant")]
    [Required]
    public virtual Etudiant Etudiant { get; set; }

    [ForeignKey("CodeEnseignant")]
    [Required]
    public virtual Enseignant Enseignant { get; set; }

    [ForeignKey("CodeMatiere")]
    [Required]
    public virtual Matiere Matiere { get; set; }
}
} 