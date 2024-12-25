using GestionAbscence.ViewModels;

namespace GestionAbscence.Services
{
    public interface IGradeService
    {
        Task<List<GradeViewModel>> GetAllGrades();
        Task<GradeViewModel> GetGradeById(string id);
        Task<bool> CreateGrade(GradeViewModel model);
        Task<bool> UpdateGrade(GradeViewModel model);
        Task<bool> DeleteGrade(string id);
    }
}
