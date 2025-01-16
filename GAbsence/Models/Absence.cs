using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAbsence.Models
{
    public class Absence
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "L'étudiant est obligatoire")]
    public string CodeEtudiant { get; set; }
    
    [Required(ErrorMessage = "La matière est obligatoire")]
    public string CodeMatiere { get; set; }
    
    [Required(ErrorMessage = "L'enseignant est obligatoire")]
    public string CodeEnseignant { get; set; }
    
    [Required]
    [Display(Name = "Date")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    [Required(ErrorMessage = "Le créneau horaire est obligatoire")]
    [Display(Name = "Créneau horaire")]
    public string CreneauHoraire { get; set; } = string.Empty;
    
    public bool EstJustifiee { get; set; }
    
    public string? Justification { get; set; }
    
    // Navigation properties
    [ForeignKey("CodeEtudiant")]
    public virtual Etudiant? Etudiant { get; set; }
    
    [ForeignKey("CodeMatiere")]
    public virtual Matiere? Matiere { get; set; }
    
    [ForeignKey("CodeEnseignant")]
    public virtual Enseignant? Enseignant { get; set; }
}
}