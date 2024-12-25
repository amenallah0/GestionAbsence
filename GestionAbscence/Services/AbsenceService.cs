using GestionAbscence.Models;
using GestionAbscence.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionAbscence.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly ApplicationDbContext _context;

        public AbsenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> MarquerAbsence(MarquerAbsenceViewModel model)
        {
            try
            {
                var ficheAbsence = new FicheAbsence
                {
                    CodeFicheAbsence = Guid.NewGuid().ToString(),
                    DateJour = model.DateSeance,
                    CodeMatiere = model.CodeMatiere,
                    CodeClasse = model.CodeClasse
                };

                _context.FicheAbsences.Add(ficheAbsence);

                foreach (var etudiant in model.Etudiants.Where(e => e.EstAbsent))
                {
                    var ligneAbsence = new LigneFicheAbsence
                    {
                        CodeFicheAbsence = ficheAbsence.CodeFicheAbsence,
                        CodeEtudiant = etudiant.CodeEtudiant
                    };
                    _context.LigneFicheAbsences.Add(ligneAbsence);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<AbsenceListViewModel>> GetAbsencesByPeriode(DateTime dateDebut, DateTime dateFin)
        {
            return await _context.FicheAbsences
                .Where(f => f.DateJour >= dateDebut && f.DateJour <= dateFin)
                .Include(f => f.LignesFicheAbsence)
                .ThenInclude(l => l.Etudiant)
                .GroupBy(f => f.LignesFicheAbsence.Select(l => l.Etudiant))
                .Select(g => new AbsenceListViewModel
                {
                    CodeEtudiant = g.Key.First().CodeEtudiant.ToString(),
                    NomEtudiant = $"{g.Key.First().Nom} {g.Key.First().Prenom}",
                    Absences = g.Select(f => new AbsenceDetail
                    {
                        Date = f.DateJour,
                        Matiere = f.Matiere.NomMatiere,
                        Enseignant = $"{f.Enseignant.Nom} {f.Enseignant.Prenom}"
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<AbsenceListViewModel> GetAbsencesByEtudiantMatiere(string codeEtudiant, string codeMatiere)
        {
            var absences = await _context.FicheAbsences
                .Where(f => f.CodeMatiere == codeMatiere && f.LignesFicheAbsence.Any(l => l.CodeEtudiant.ToString() == codeEtudiant))
                .Include(f => f.LignesFicheAbsence)
                .ThenInclude(l => l.Etudiant)
                .Select(f => new AbsenceDetail
                {
                    Date = f.DateJour,
                    Matiere = f.Matiere.NomMatiere,
                    Enseignant = $"{f.Enseignant.Nom} {f.Enseignant.Prenom}"
                })
                .ToListAsync();

            var etudiant = await _context.Etudiants.FindAsync(int.Parse(codeEtudiant));

            return new AbsenceListViewModel
            {
                CodeEtudiant = etudiant.CodeEtudiant.ToString(),
                NomEtudiant = $"{etudiant.Nom} {etudiant.Prenom}",
                Absences = absences
            };
        }

        public async Task<List<AbsenceListViewModel>> GetAbsencesJournalieres(DateTime date)
        {
            return await _context.FicheAbsences
                .Where(f => f.DateJour == date)
                .Include(f => f.LignesFicheAbsence)
                .ThenInclude(l => l.Etudiant)
                .GroupBy(f => f.LignesFicheAbsence.Select(l => l.Etudiant))
                .Select(g => new AbsenceListViewModel
                {
                    CodeEtudiant = g.Key.First().CodeEtudiant.ToString(),
                    NomEtudiant = $"{g.Key.First().Nom} {g.Key.First().Prenom}",
                    Absences = g.Select(f => new AbsenceDetail
                    {
                        Date = f.DateJour,
                        Matiere = f.Matiere.NomMatiere,
                        Enseignant = $"{f.Enseignant.Nom} {f.Enseignant.Prenom}"
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
