namespace GAbsence.Models
{
    public class Matiere
    {
        public required string CodeMatiere { get; set; }
        public required string NomMatiere { get; set; }
        public int NbHeures { get; set; }
        public virtual ICollection<Seance>? Seances { get; set; }
    }
} 