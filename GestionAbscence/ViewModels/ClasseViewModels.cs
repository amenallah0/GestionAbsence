namespace GestionAbscence.ViewModels
{
    public class ClasseListViewModel
    {
        public string CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public int NombreEtudiants { get; set; }
    }

    public class ClasseDetailsViewModel
    {
        public string CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public List<EtudiantViewModel> Etudiants { get; set; }
        public string CodeDepartement { get; set; }
    }

    public class ClasseStatisticsViewModel
    {
        public string CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public int TotalAbsences { get; set; }
        public double TauxAbsenteisme { get; set; }
    }

    public class ClasseEmploiDuTempsViewModel
    {
        public string CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public List<SeanceViewModel> Seances { get; set; }
    }

    public class ClasseCreateEditViewModel
    {
        public string CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public string CodeDepartement { get; set; }
        public List<string> MatiereIds { get; set; }
    }
} 