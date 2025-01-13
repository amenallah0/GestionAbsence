using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAbsence.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClasseController> _logger;

        public ClasseController(ApplicationDbContext context, ILogger<ClasseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Classe
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes.Include(c => c.Etudiants).ToListAsync();
            return View(classes);
        }

        // GET: Classe/Create
        public IActionResult Create()
        {
            // Charger la liste des filières pour le dropdown
            ViewBag.Filieres = new SelectList(_context.Filieres, "CodeFiliere", "NomFiliere");
            return View();
        }

        // POST: Classe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeClasse,NomClasse,CodeFiliere,Niveau")] Classe classe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(classe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erreur lors de la création de la classe: {ex.Message}");
                    ModelState.AddModelError("", "Une erreur est survenue lors de la création de la classe.");
                }
            }
            else
            {
                // Log les erreurs de validation pour le débogage
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"Erreur de validation: {error.ErrorMessage}");
                    }
                }
            }
            
            // En cas d'erreur, recharger la liste des filières
            ViewBag.Filieres = new SelectList(_context.Filieres, "CodeFiliere", "NomFiliere", classe.CodeFiliere);
            return View(classe);
        }

        // GET: Classe/Edit/GL2024
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes
                .Include(c => c.Filiere)
                .Include(c => c.Etudiants)
                .FirstOrDefaultAsync(c => c.CodeClasse == id);

            if (classe == null)
            {
                return NotFound();
            }

            // Charger la liste des filières pour le dropdown
            ViewBag.Filieres = await _context.Filieres
                .OrderBy(f => f.NomFiliere)
                .Select(f => new
                {
                    CodeFiliere = f.CodeFiliere,
                    NomComplet = $"{f.CodeFiliere} - {f.NomFiliere}"
                })
                .ToListAsync();

            return View(classe);
        }

        // POST: Classe/Edit/GL2024
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Classe classe)
        {
            if (id != classe.CodeClasse)
            {
                return NotFound();
            }

            try
            {
                var existingClasse = await _context.Classes
                    .Include(c => c.Etudiants)
                    .FirstOrDefaultAsync(c => c.CodeClasse == id);

                if (existingClasse == null)
                {
                    return NotFound();
                }

                // Mettre à jour les propriétés
                existingClasse.Niveau = classe.Niveau;
                existingClasse.CodeFiliere = classe.CodeFiliere;

                await _context.SaveChangesAsync();
                _logger.LogInformation($"Classe {id} mise à jour avec succès");
                TempData["Success"] = "La classe a été modifiée avec succès.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de la modification de la classe {id}: {ex.Message}");
                ModelState.AddModelError("", "Une erreur est survenue lors de la modification de la classe.");
            }

            // Recharger les données en cas d'erreur
            ViewBag.Filieres = await _context.Filieres
                .OrderBy(f => f.NomFiliere)
                .Select(f => new
                {
                    CodeFiliere = f.CodeFiliere,
                    NomComplet = $"{f.CodeFiliere} - {f.NomFiliere}"
                })
                .ToListAsync();

            return View(classe);
        }

        private bool ClasseExists(string id)
        {
            return _context.Classes.Any(e => e.CodeClasse == id);
        }

        // GET: Classe/Details/DSI31
        public async Task<IActionResult> Details(string id)
        {
            var classe = await _context.Classes
                .Include(c => c.Etudiants)
                .FirstOrDefaultAsync(c => c.CodeClasse == id);

            if (classe?.Etudiants == null)
            {
                return NotFound();
            }

            var absencesParEtudiant = await _context.Absences
                .Where(a => a.Etudiant != null && a.Etudiant.CodeClasse == id)
                .GroupBy(a => new { 
                    a.CodeEtudiant, 
                    Nom = a.Etudiant.Nom, 
                    Prenom = a.Etudiant.Prenom 
                })
                .Select(g => new
                {
                    Etudiant = g.Key,
                    TotalAbsences = g.Count(),
                    AbsencesJustifiees = g.Count(a => a.EstJustifiee),
                    AbsencesNonJustifiees = g.Count(a => !a.EstJustifiee)
                })
                .ToListAsync();

            ViewBag.AbsencesParEtudiant = absencesParEtudiant;
            return View(classe);
        }
    }
} 