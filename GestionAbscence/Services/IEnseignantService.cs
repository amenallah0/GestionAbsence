using GestionAbscence.ViewModels;

namespace GestionAbscence.Services
{
    public interface IEnseignantService
    {
        Task<List<EnseignantViewModel>> GetAllEnseignants();
        Task<EnseignantViewModel> GetEnseignantById(string id);
        Task<bool> CreateEnseignant(EnseignantViewModel model);
        Task<bool> UpdateEnseignant(EnseignantViewModel model);
        Task<bool> DeleteEnseignant(string id);
    }
}
