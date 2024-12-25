namespace GestionAbscence.ViewModels
{
    public class SeanceViewModel
    {
        public int Id { get; set; }
        public string CodeMatiere { get; set; }
        public string NomMatiere { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string CodeEnseignant { get; set; }
        public string NomEnseignant { get; set; }
    }
} 