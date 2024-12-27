namespace GAbsence.Models
{
    public class Grade
    {
        public required string CodeGrade { get; set; }
        public required string NomGrade { get; set; }
        public virtual ICollection<Enseignant>? Enseignants { get; set; }
    }
} 