namespace GestionAbscence.Models
{
    public class LigneFicheAbsence
    {
        public string CodeFicheAbsence { get; set; }
        public int CodeEtudiant { get; set; }
        public bool Justifie { get; set; }
        public string? Justification { get; set; }

        // Relations
        public virtual FicheAbsence FicheAbsence { get; set; }
        public virtual Etudiant Etudiant { get; set; }
    }
}
