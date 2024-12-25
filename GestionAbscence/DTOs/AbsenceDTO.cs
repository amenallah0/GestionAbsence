using System.ComponentModel.DataAnnotations;

namespace GestionAbscence.DTOs
{
    public class AbsenceDTO
    {
        public int Id { get; set; }
        public int EtudiantId { get; set; }
        public string NomEtudiant { get; set; }
        public DateTime DateAbsence { get; set; }
        public string Matiere { get; set; }
        public bool EstJustifiee { get; set; }
        public string? Justification { get; set; }
    }

    public class CreateAbsenceDTO
    {
        [Required]
        public int EtudiantId { get; set; }
        
        [Required]
        public int MatiereId { get; set; }
        
        [Required]
        public DateTime DateAbsence { get; set; }
    } 
} 