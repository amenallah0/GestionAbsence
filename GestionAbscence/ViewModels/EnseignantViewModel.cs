using System.ComponentModel.DataAnnotations;

namespace GestionAbscence.ViewModels
{
    public class EnseignantViewModel
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
    }
}
