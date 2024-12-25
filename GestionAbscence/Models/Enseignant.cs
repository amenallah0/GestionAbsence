using System.Diagnostics;

namespace GestionAbscence.Models
{
    public class Enseignant
    {
        public string CodeEnseignant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateRecrutement { get; set; }
        public string Adresse { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
        public string CodeDepartement { get; set; }
        public string CodeGrade { get; set; }

        // Relations
        public virtual Departement Departement { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ICollection<FicheAbsence> FicheAbsences { get; set; }
    }
}
