using GestionAbscence.ViewModels;

namespace GestionAbscence.Services
{
    public interface IMatiereService
    {
        Task<IEnumerable<MatiereViewModel>> GetAllMatieres();
        Task<MatiereViewModel?> GetMatiereDetails(string id);
        Task<bool> CreateMatiere(MatiereCreateEditViewModel model);
        Task<MatiereCreateEditViewModel?> GetMatiereForEdit(string id);
        Task<bool> UpdateMatiere(MatiereCreateEditViewModel model);
        Task<MatiereStatisticsViewModel?> GetMatiereStatistics(string id);
    }
}
