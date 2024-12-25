namespace GestionAbscence.Models
{
    public class Seance
    {
        public required string CodeSeance { get; set; }
        public required string NomSeance { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }

        // Relations
        public virtual ICollection<FicheAbsence> FicheAbsenceSeances { get; set; }
    }
}
