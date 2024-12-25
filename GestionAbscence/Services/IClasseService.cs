using GestionAbscence.ViewModels;

namespace GestionAbscence.Services
{
    public interface IClasseService
    {
        Task<IEnumerable<ClasseListViewModel>> GetAllClasses();
        Task<ClasseDetailsViewModel?> GetClasseDetails(string id);
        Task<ClasseCreateEditViewModel> PrepareClasseCreateEditViewModel();
        Task<bool> CreateClasse(ClasseCreateEditViewModel model);
        Task<ClasseStatisticsViewModel?> GetClasseStatistics(string id);
        Task<ClasseEmploiDuTempsViewModel?> GetClasseEmploiDuTemps(string id);
    }
}
