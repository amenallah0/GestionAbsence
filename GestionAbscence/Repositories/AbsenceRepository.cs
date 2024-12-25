using GestionAbscence.Models;
using GestionAbscence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionAbscence.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly ApplicationDbContext _context;

        public AbsenceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Absence>> GetAllAsync()
        {
            return await _context.Absences
                .Include(a => a.Etudiant)
                .Include(a => a.Matiere)
                .ToListAsync();
        }

        public async Task<Absence> GetByIdAsync(int id)
        {
            return await _context.Absences
                .Include(a => a.Etudiant)
                .Include(a => a.Matiere)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Absence>> GetByEtudiantIdAsync(int etudiantId)
        {
            return await _context.Absences
                .Include(a => a.Matiere)
                .Where(a => a.EtudiantId == etudiantId)
                .ToListAsync();
        }

        public async Task<Absence> CreateAsync(Absence absence)
        {
            await _context.Absences.AddAsync(absence);
            await _context.SaveChangesAsync();
            return absence;
        }

        public async Task UpdateAsync(Absence absence)
        {
            try
            {
                var existingAbsence = await _context.Absences.FindAsync(absence.Id);
                if (existingAbsence == null)
                    throw new KeyNotFoundException($"Absence with ID {absence.Id} not found");

                _context.Entry(existingAbsence).CurrentValues.SetValues(absence);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence != null)
            {
                _context.Absences.Remove(absence);
                await _context.SaveChangesAsync();
            }
        }
    }
}