namespace GestionAbscence.Models
{
    public class Grade
    {
        public string CodeGrade { get; set; }
        public string NomGrade { get; set; }
        public string Description { get; set; }

        // Relations
        public virtual ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>();
    }
}
