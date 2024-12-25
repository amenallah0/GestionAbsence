namespace GestionAbscence.Models
{
    public class FicheAbsence
    {
        public string CodeFicheAbsence { get; set; }
        public DateTime DateJour { get; set; }
        public string CodeMatiere { get; set; }
        public string CodeEnseignant { get; set; }
        public string CodeClasse { get; set; }

        // Relations
        public virtual Matiere Matiere { get; set; }
        public virtual Enseignant Enseignant { get; set; }
        public virtual Classe Classe { get; set; }
        public virtual ICollection<LigneFicheAbsence> LignesFicheAbsence { get; set; }
    }
}
