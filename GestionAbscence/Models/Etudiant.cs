using System.Collections.Generic; // Pour ICollection
using System.ComponentModel.DataAnnotations; // Pour le modificateur required
using GestionAbscence.Models; // Pour Classe et LigneFicheAbsence

namespace GestionAbscence.Models
{
    public class Etudiant
    {
        public int CodeEtudiant { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public DateTime DateNaissance { get; set; }
        public string CodeClasse { get; set; } = string.Empty;
        public string NumInscription { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        
        public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();
        public virtual Classe Classe { get; set; }
    }
}
