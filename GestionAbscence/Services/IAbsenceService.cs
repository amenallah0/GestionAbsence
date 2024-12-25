using GestionAbscence.ViewModels;

namespace GestionAbscence.Services
{
    public interface IAbsenceService
    {
        Task<bool> MarquerAbsence(MarquerAbsenceViewModel model);
        Task<List<AbsenceListViewModel>> GetAbsencesByPeriode(DateTime dateDebut, DateTime dateFin);
        Task<AbsenceListViewModel> GetAbsencesByEtudiantMatiere(string codeEtudiant, string codeMatiere);
        Task<List<AbsenceListViewModel>> GetAbsencesJournalieres(DateTime date);
    }
}
