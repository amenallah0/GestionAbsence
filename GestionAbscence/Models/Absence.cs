using System.ComponentModel.DataAnnotations;

namespace GestionAbscence.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public int EtudiantId { get; set; }
        public DateTime DateSeance { get; set; }
        public string CodeMatiere { get; set; }
        public bool EstJustifie { get; set; }
        public string? Justification { get; set; }
        
        public virtual Etudiant Etudiant { get; set; }
        public virtual Matiere Matiere { get; set; }
    }
} 