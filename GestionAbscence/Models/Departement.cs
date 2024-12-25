namespace GestionAbscence.Models
{
    public class Departement
    {
        public string CodeDepartement { get; set; }
        public string NomDepartement { get; set; }

        // Relations
        public virtual ICollection<Classe> Classes { get; set; }
        public virtual ICollection<Enseignant> Enseignants { get; set; }
    }
}
