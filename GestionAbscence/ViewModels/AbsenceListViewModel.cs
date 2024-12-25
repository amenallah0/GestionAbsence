namespace GestionAbscence.ViewModels
{
    public class AbsenceListViewModel
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string CodeEtudiant { get; set; }
        public string NomEtudiant { get; set; }
        public List<AbsenceDetail> Absences { get; set; }
    }

    public class AbsenceDetail
    {
        public DateTime Date { get; set; }
        public string Matiere { get; set; }
        public string Seance { get; set; }
        public string Enseignant { get; set; }
    }
}
