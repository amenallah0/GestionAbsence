using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GAbsence.Models;
using GAbsence.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAbsence.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AbsenceController> _logger;

        public AbsenceController(ApplicationDbContext context, ILogger<AbsenceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private void LoadDropDownLists()
        {
            ViewBag.Etudiants = new SelectList(_context.Etudiants
                .Select(e => new { e.CodeEtudiant, NomComplet = $"{e.Nom} {e.Prenom}" }), 
                "CodeEtudiant", 
                "NomComplet");

            ViewBag.Matieres = new SelectList(_context.Matieres, 
                "CodeMatiere", 
                "Libelle");

            ViewBag.Enseignants = new SelectList(_context.Enseignants
                .Select(e => new { e.CodeEnseignant, NomComplet = $"{e.Nom} {e.Prenom}" }), 
                "CodeEnseignant", 
                "NomComplet");

            // Définir les créneaux horaires comme des SelectListItem
            ViewBag.CreneauxHoraires = new[]
            {
                new SelectListItem { Value = "08:00-10:00", Text = "08:00 - 10:00" },
                new SelectListItem { Value = "10:00-12:00", Text = "10:00 - 12:00" },
                new SelectListItem { Value = "14:00-16:00", Text = "14:00 - 16:00" },
                new SelectListItem { Value = "16:00-18:00", Text = "16:00 - 18:00" }
            };
        }

        // GET: Absence/Create
        public IActionResult Create()
        {
            LoadDropDownLists();
            return View();
        }

        // POST: Absence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeEtudiant,CodeMatiere,CodeEnseignant,Date,CreneauHoraire,EstJustifiee,Justification")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(absence);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Une erreur s'est produite lors de l'enregistrement.");
                }
            }

            // Recharger les listes en cas d'erreur
            LoadDropDownLists();
            return View(absence);
        }

        // GET: Absence
        public async Task<IActionResult> Index()
        {
            var absences = await _context.Absences
                .Include(a => a.Etudiant)
                .Include(a => a.Matiere)
                .Include(a => a.Enseignant)
                .ToListAsync();

            return View(absences);
        }

        // GET: Absence/Rapport
        public IActionResult Rapport()
        {
            // Préparer la liste des étudiants pour la liste déroulante
            ViewBag.Etudiants = new SelectList(_context.Etudiants
                .OrderBy(e => e.Nom)
                .Select(e => new
                {
                    e.CodeEtudiant,
                    NomComplet = $"{e.Nom} {e.Prenom} ({e.CodeEtudiant})"
                }), "CodeEtudiant", "NomComplet");

            return View();
        }

        // GET: Absence/RapportParEtudiant
        public async Task<IActionResult> RapportParEtudiant(string codeEtudiant)
        {
            if (string.IsNullOrEmpty(codeEtudiant))
            {
                _logger.LogWarning("Code étudiant est vide");
                return NotFound();
            }

            _logger.LogInformation($"Recherche des absences pour l'étudiant: {codeEtudiant}");

            // Vérifier si l'étudiant existe
            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.CodeEtudiant == codeEtudiant);

            if (etudiant == null)
            {
                _logger.LogWarning($"Étudiant non trouvé avec le code: {codeEtudiant}");
                return NotFound();
            }

            // Récupérer toutes les absences avec une requête SQL explicite pour le débogage
            var absences = await _context.Absences
                .Where(a => a.CodeEtudiant == codeEtudiant)
                .Include(a => a.Etudiant)
                .Include(a => a.Enseignant)
                .Include(a => a.Matiere)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            // Log pour débogage
            _logger.LogInformation($"SQL Query: {_context.Absences.Where(a => a.CodeEtudiant == codeEtudiant).ToQueryString()}");
            _logger.LogInformation($"Nombre d'absences trouvées: {absences.Count}");
            
            foreach (var absence in absences)
            {
                _logger.LogInformation($"Absence trouvée - Date: {absence.Date}, Matière: {absence.Matiere?.Libelle}, Enseignant: {absence.Enseignant?.Nom}");
            }

            ViewBag.Etudiant = etudiant;
            ViewBag.TotalAbsences = absences.Count;
            ViewBag.AbsencesJustifiees = absences.Count(a => a.EstJustifiee);
            ViewBag.AbsencesNonJustifiees = absences.Count(a => !a.EstJustifiee);

            return View(absences);
        }

        // GET: Absence/RapportParPeriode
        public async Task<IActionResult> RapportParPeriode(DateTime dateDebut, DateTime dateFin)
        {
            var absences = await _context.Absences
                .Include(a => a.Etudiant)
                    .ThenInclude(e => e.Classe)
                .Where(a => a.Date >= dateDebut && a.Date <= dateFin)
                .OrderBy(a => a.Date)
                .ToListAsync();

            ViewBag.DateDebut = dateDebut;
            ViewBag.DateFin = dateFin;
            return View(absences);
        }

        // GET: Absence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences
                .Include(a => a.Etudiant)
                .Include(a => a.Enseignant)
                .Include(a => a.Matiere)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        // GET: Absence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
            {
                return NotFound();
            }

            ViewBag.Etudiants = new SelectList(_context.Etudiants, "CodeEtudiant", "NomComplet");
            ViewBag.Enseignants = new SelectList(_context.Enseignants, "CodeEnseignant", "NomComplet");
            ViewBag.Matieres = new SelectList(_context.Matieres, "CodeMatiere", "Libelle");
            ViewBag.Creneaux = new List<string> { "8h-10h", "10h-12h", "14h-16h", "16h-18h" };

            return View(absence);
        }

        // POST: Absence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CreneauHoraire,CodeEtudiant,CodeEnseignant,CodeMatiere,EstJustifiee,Justification")] Absence absence)
        {
            if (id != absence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absence);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceExists(absence.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Etudiants = new SelectList(_context.Etudiants, "CodeEtudiant", "NomComplet");
            ViewBag.Enseignants = new SelectList(_context.Enseignants, "CodeEnseignant", "NomComplet");
            ViewBag.Matieres = new SelectList(_context.Matieres, "CodeMatiere", "Libelle");
            ViewBag.Creneaux = new List<string> { "8h-10h", "10h-12h", "14h-16h", "16h-18h" };

            return View(absence);
        }

        // POST: Absence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence != null)
            {
                _context.Absences.Remove(absence);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceExists(int id)
        {
            return _context.Absences.Any(e => e.Id == id);
        }
    }
} 