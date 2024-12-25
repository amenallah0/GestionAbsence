using GestionAbscence.Models;
using GestionAbscence.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionAbscence.Services
{
    public class ClasseService : IClasseService
    {
        private readonly ApplicationDbContext _context;

        public ClasseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClasseListViewModel>> GetAllClasses()
        {
            return await _context.Classes
                .Select(c => new ClasseListViewModel
                {
                    CodeClasse = c.CodeClasse,
                    NomClasse = c.NomClasse,
                    NombreEtudiants = c.Etudiants.Count
                })
                .ToListAsync();
        }

        public async Task<ClasseDetailsViewModel?> GetClasseDetails(string id)
        {
            var classe = await _context.Classes
                .Include(c => c.Etudiants)
                .FirstOrDefaultAsync(c => c.CodeClasse == id);

            if (classe == null) return null;

            return new ClasseDetailsViewModel
            {
                CodeClasse = classe.CodeClasse,
                NomClasse = classe.NomClasse,
                CodeDepartement = classe.CodeDepartement,
                Etudiants = classe.Etudiants.Select(e => new EtudiantViewModel
                {
                    CodeEtudiant = e.CodeEtudiant,
                    Nom = e.Nom,
                    Prenom = e.Prenom
                }).ToList()
            };
        }

        public async Task<ClasseCreateEditViewModel> PrepareClasseCreateEditViewModel()
        {
            return new ClasseCreateEditViewModel
            {
                MatiereIds = await _context.Matieres.Select(m => m.CodeMatiere).ToListAsync()
            };
        }

        public async Task<bool> CreateClasse(ClasseCreateEditViewModel model)
        {
            var classe = new Classe
            {
                CodeClasse = model.CodeClasse,
                NomClasse = model.NomClasse,
                CodeDepartement = model.CodeDepartement
            };

            _context.Classes.Add(classe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ClasseStatisticsViewModel?> GetClasseStatistics(string id)
        {
            var classe = await _context.Classes
                .Include(c => c.Etudiants)
                .ThenInclude(e => e.Absences)
                .FirstOrDefaultAsync(c => c.CodeClasse == id);

            if (classe == null) return null;

            return new ClasseStatisticsViewModel
            {
                CodeClasse = classe.CodeClasse,
                NomClasse = classe.NomClasse,
                TotalAbsences = classe.Etudiants.Sum(e => e.Absences.Count),
                TauxAbsenteisme = CalculerTauxAbsenteisme(classe)
            };
        }

        private double CalculerTauxAbsenteisme(Classe classe)
        {
            // Logique de calcul du taux d'absentéisme
            return 0.0; // À implémenter selon vos besoins
        }

        public async Task<ClasseEmploiDuTempsViewModel?> GetClasseEmploiDuTemps(string id)
        {
            var classe = await _context.Classes
                .FirstOrDefaultAsync(c => c.CodeClasse == id);

            if (classe == null) return null;

            return new ClasseEmploiDuTempsViewModel
            {
                CodeClasse = classe.CodeClasse,
                NomClasse = classe.NomClasse,
                Seances = new List<SeanceViewModel>() // À implémenter selon vos besoins
            };
        }
    }
} 