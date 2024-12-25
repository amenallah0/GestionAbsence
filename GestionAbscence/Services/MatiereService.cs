using GestionAbscence.Models;
using GestionAbscence.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionAbscence.Services
{
    public class MatiereService : IMatiereService
    {
        private readonly ApplicationDbContext _context;

        public MatiereService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MatiereViewModel>> GetAllMatieres()
        {
            return await _context.Matieres
                .Select(m => new MatiereViewModel
                {
                    CodeMatiere = m.CodeMatiere,
                    NomMatiere = m.NomMatiere,
                    NbHeures = m.NbHeures
                })
                .ToListAsync();
        }

        public async Task<MatiereViewModel?> GetMatiereDetails(string id)
        {
            var matiere = await _context.Matieres
                .FirstOrDefaultAsync(m => m.CodeMatiere == id);

            if (matiere == null) return null;

            return new MatiereViewModel
            {
                CodeMatiere = matiere.CodeMatiere,
                NomMatiere = matiere.NomMatiere,
                NbHeures = matiere.NbHeures
            };
        }

        public async Task<bool> CreateMatiere(MatiereCreateEditViewModel model)
        {
            var matiere = new Matiere
            {
                CodeMatiere = model.CodeMatiere,
                NomMatiere = model.NomMatiere,
                NbHeures = model.NbHeures,
                CodeDepartement = model.CodeDepartement
            };

            _context.Matieres.Add(matiere);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MatiereCreateEditViewModel?> GetMatiereForEdit(string id)
        {
            var matiere = await _context.Matieres
                .FirstOrDefaultAsync(m => m.CodeMatiere == id);

            if (matiere == null) return null;

            return new MatiereCreateEditViewModel
            {
                CodeMatiere = matiere.CodeMatiere,
                NomMatiere = matiere.NomMatiere,
                NbHeures = matiere.NbHeures,
                CodeDepartement = matiere.CodeDepartement
            };
        }

        public async Task<bool> UpdateMatiere(MatiereCreateEditViewModel model)
        {
            var matiere = await _context.Matieres
                .FirstOrDefaultAsync(m => m.CodeMatiere == model.CodeMatiere);

            if (matiere == null) return false;

            matiere.NomMatiere = model.NomMatiere;
            matiere.NbHeures = model.NbHeures;
            matiere.CodeDepartement = model.CodeDepartement;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MatiereStatisticsViewModel?> GetMatiereStatistics(string id)
        {
            var matiere = await _context.Matieres
                .Include(m => m.Absences)
                .FirstOrDefaultAsync(m => m.CodeMatiere == id);

            if (matiere == null) return null;

            return new MatiereStatisticsViewModel
            {
                CodeMatiere = matiere.CodeMatiere,
                NomMatiere = matiere.NomMatiere,
                TotalAbsences = matiere.Absences.Count,
                TauxAbsenteisme = CalculerTauxAbsenteisme(matiere)
            };
        }

        private double CalculerTauxAbsenteisme(Matiere matiere)
        {
            // Logique de calcul du taux d'absentéisme
            return 0.0; // À implémenter selon vos besoins
        }
    }
} 