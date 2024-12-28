using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAbsence.Models;

namespace GAbsence.Controllers
{
    public class RapportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RapportController> _logger;

        public RapportController(ApplicationDbContext context, ILogger<RapportController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Rapport
        public IActionResult Index()
        {
            // Utilisation du CodeEtudiant au lieu de Id
            ViewBag.Etudiants = new SelectList(_context.Etudiants
                .OrderBy(e => e.Nom)
                .Select(e => new
                {
                    CodeEtudiant = e.CodeEtudiant,  // Utiliser CodeEtudiant
                    NomComplet = $"{e.Nom} {e.Prenom} ({e.CodeEtudiant})"
                }), "CodeEtudiant", "NomComplet");  // Utiliser CodeEtudiant comme valeur

            return View();
        }

        // GET: Rapport/ParEtudiant
        public async Task<IActionResult> ParEtudiant(string codeEtudiant)  // Renommé de 'id' à 'codeEtudiant'
        {
            if (string.IsNullOrEmpty(codeEtudiant))
            {
                return NotFound();
            }

            _logger.LogInformation($"Recherche de l'étudiant avec le code: {codeEtudiant}");

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.CodeEtudiant == codeEtudiant);  // Utiliser CodeEtudiant

            if (etudiant == null)
            {
                _logger.LogWarning($"Aucun étudiant trouvé avec le code: {codeEtudiant}");
                return NotFound();
            }

            var absences = await _context.Absences
                .Where(a => a.CodeEtudiant == codeEtudiant)  // Utiliser CodeEtudiant
                .Include(a => a.Enseignant)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            _logger.LogInformation($"Nombre d'absences trouvées: {absences.Count}");

            ViewBag.Etudiant = etudiant;
            return View(absences);
        }

        // GET: Rapport/ParPeriode
        public async Task<IActionResult> ParPeriode(DateTime? dateDebut, DateTime? dateFin)
        {
            if (!dateDebut.HasValue)
                dateDebut = DateTime.Today.AddMonths(-1);
            if (!dateFin.HasValue)
                dateFin = DateTime.Today;

            var absences = await _context.Absences
                .Include(a => a.Etudiant)
                    .ThenInclude(e => e.Classe)
                .Include(a => a.Enseignant)
                .Where(a => a.Date >= dateDebut && a.Date <= dateFin)
                .OrderByDescending(a => a.Date)
                .ThenBy(a => a.Etudiant.Nom)
                .ToListAsync();

            ViewBag.DateDebut = dateDebut.Value;
            ViewBag.DateFin = dateFin.Value;
            ViewBag.TotalAbsences = absences.Count;
            ViewBag.AbsencesJustifiees = absences.Count(a => a.EstJustifiee);

            return View(absences);
        }

        public async Task<IActionResult> RapportParEtudiant(string etudiantId)
        {
            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.CodeEtudiant == etudiantId);

            if (etudiant == null)
            {
                return NotFound();
            }

            // Récupérer les absences des deux tables
            var absences = await _context.Absences
                .Include(a => a.Etudiant)
                    .ThenInclude(e => e.Classe)
                .Include(a => a.Enseignant)
                .Include(a => a.Matiere)
                .Where(a => a.CodeEtudiant == etudiantId)
                .ToListAsync();

            var ficheAbsences = await _context.FicheAbsences
                .Include(f => f.Etudiant)
                .Include(f => f.Enseignant)
                .Where(f => f.CodeEtudiant == etudiantId)
                .ToListAsync();

            // Combiner les résultats
            ViewBag.Etudiant = etudiant;
            ViewBag.TotalAbsences = absences.Count + ficheAbsences.Count;
            ViewBag.AbsencesJustifiees = absences.Count(a => a.EstJustifiee) + 
                                        ficheAbsences.Count(f => f.EstJustifiee);
            ViewBag.AbsencesNonJustifiees = absences.Count(a => !a.EstJustifiee) + 
                                           ficheAbsences.Count(f => !f.EstJustifiee);

            // Créer un modèle combiné pour la vue
            var combinedAbsences = new List<dynamic>();
            
            foreach (var absence in absences)
            {
                combinedAbsences.Add(new
                {
                    Date = absence.Date,
                    CreneauHoraire = absence.CreneauHoraire,
                    Matiere = absence.Matiere?.Libelle,
                    Enseignant = $"{absence.Enseignant?.Nom} {absence.Enseignant?.Prenom}",
                    EstJustifiee = absence.EstJustifiee,
                    Justification = absence.Justification,
                    Source = "Absence"
                });
            }

            foreach (var fiche in ficheAbsences)
            {
                combinedAbsences.Add(new
                {
                    Date = fiche.Date,
                    CreneauHoraire = fiche.CreneauHoraire,
                    Matiere = fiche.Matiere,
                    Enseignant = $"{fiche.Enseignant?.Nom} {fiche.Enseignant?.Prenom}",
                    EstJustifiee = fiche.EstJustifiee,
                    Justification = fiche.Justification,
                    Source = "FicheAbsence"
                });
            }

            return View(combinedAbsences.OrderByDescending(a => a.Date).ToList());
        }
    }
} 