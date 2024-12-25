using System.ComponentModel.DataAnnotations;

namespace GestionAbscence.ViewModels
{
    public class EtudiantViewModel
    {
        public int CodeEtudiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string CodeClasse { get; set; }
        public string NumInscription { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
    }
}
