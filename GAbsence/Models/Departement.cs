namespace GAbsence.Models
{
    public class Departement
    {
        public required string CodeDepartement { get; set; }
        public required string NomDepartement { get; set; }
        public virtual ICollection<Enseignant>? Enseignants { get; set; }
    }
} 