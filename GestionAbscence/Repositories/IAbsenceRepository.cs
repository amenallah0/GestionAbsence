public interface IAbsenceRepository
{
    Task<IEnumerable<Absence>> GetAllAsync();
    Task<Absence> GetByIdAsync(int id);
    Task<IEnumerable<Absence>> GetByEtudiantIdAsync(int etudiantId);
    Task<Absence> CreateAsync(Absence absence);
    Task UpdateAsync(Absence absence);
    Task DeleteAsync(int id);
} 