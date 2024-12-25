namespace GestionAbscence.Models
{
    public class Matiere
    {
        public string CodeMatiere { get; set; }
        public string NomMatiere { get; set; }
        public int NbHeures { get; set; }
        public string CodeDepartement { get; set; }
        
        public virtual ICollection<Absence> Absences { get; set; }
    }
} 