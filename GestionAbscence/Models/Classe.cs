namespace GestionAbscence.Models
{
    public class Classe
    {
        public string CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public string CodeDepartement { get; set; }
        public virtual ICollection<Etudiant> Etudiants { get; set; } = new List<Etudiant>();
        public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
    }
}
