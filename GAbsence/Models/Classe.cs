using System.Collections.Generic;
using GAbsence.Models;

namespace GAbsence.Models
{
    public class Classe
    {
        public required string CodeClasse { get; set; }
        public required string NomClasse { get; set; }
        public virtual ICollection<Etudiant>? Etudiants { get; set; }
        public virtual ICollection<Groupe>? Groupes { get; set; }
    }
} 