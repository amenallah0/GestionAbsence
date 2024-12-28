namespace GAbsence.Models
{
    public class Departement
    {
        public string CodeDepartement { get; set; }
        public string NomDepartement { get; set; }
        public DateTime? DateCreation { get; set; }
        public virtual ICollection<Classe> Classes { get; set; }
        public virtual ICollection<Enseignant> Enseignants { get; set; }

        public Departement()
        {
            Classes = new HashSet<Classe>();
            Enseignants = new HashSet<Enseignant>();
        }
    }
} 