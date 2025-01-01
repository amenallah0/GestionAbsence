using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GAbsence.Controllers
{
    public class EtudiantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EtudiantController> _logger;

        public EtudiantController(ApplicationDbContext context, ILogger<EtudiantController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Etudiant
        public async Task<IActionResult> Index()
        {
            var etudiants = await _context.Etudiants
                .Include(e => e.Classe)
                .ToListAsync();
            return View(etudiants);
        }

        // GET: Etudiant/Create
        public IActionResult Create()
        {
            ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse");
            return View();
        }

        // POST: Etudiant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeEtudiant,Nom,Prenom,DateNaissance,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
        {
            _logger.LogInformation("Tentative de création d'un étudiant");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState invalide");
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Erreur: {modelError.ErrorMessage}");
                }
                ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse", etudiant.CodeClasse);
                return View(etudiant);
            }

            try
            {
                // Vérifier si le code étudiant existe déjà
                if (await _context.Etudiants.AnyAsync(e => e.CodeEtudiant == etudiant.CodeEtudiant))
                {
                    _logger.LogWarning($"Le code étudiant {etudiant.CodeEtudiant} existe déjà");
                    ModelState.AddModelError("CodeEtudiant", "Ce code étudiant existe déjà");
                    ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse", etudiant.CodeClasse);
                    return View(etudiant);
                }

                _context.Add(etudiant);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Étudiant créé avec succès: {etudiant.CodeEtudiant}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de la création de l'étudiant: {ex.Message}");
                ModelState.AddModelError("", "Une erreur s'est produite lors de la création de l'étudiant");
                ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse", etudiant.CodeClasse);
                return View(etudiant);
            }
        }

        // GET: Etudiant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.CodeEtudiant == id);

            if (etudiant == null)
            {
                return NotFound();
            }

            ViewBag.Classes = await _context.Classes
                .OrderBy(c => c.CodeClasse)
                .Select(c => new
                {
                    CodeClasse = c.CodeClasse,
                    DisplayText = $"{c.CodeClasse} - {c.Niveau} "
                })
                .ToListAsync();

            return View(etudiant);
        }

        // POST: Etudiant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeEtudiant,Nom,Prenom,DateNaissance,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
        {
            if (id != etudiant.CodeEtudiant)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEtudiant = await _context.Etudiants.FindAsync(id);
                    if (existingEtudiant == null)
                    {
                        return NotFound();
                    }

                    // Mettre à jour uniquement les propriétés modifiables
                    existingEtudiant.Nom = etudiant.Nom;
                    existingEtudiant.Prenom = etudiant.Prenom;
                    existingEtudiant.DateNaissance = etudiant.DateNaissance;
                    existingEtudiant.Adresse = etudiant.Adresse;
                    existingEtudiant.Mail = etudiant.Mail;
                    existingEtudiant.Tel = etudiant.Tel;
                    existingEtudiant.CodeClasse = etudiant.CodeClasse;

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Étudiant modifié avec succès: {etudiant.CodeEtudiant}");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!EtudiantExists(etudiant.CodeEtudiant))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError($"Erreur lors de la modification de l'étudiant: {ex.Message}");
                        throw;
                    }
                }
            }
            else
            {
                // Log des erreurs de validation
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Erreur de validation: {modelError.ErrorMessage}");
                }
            }

            ViewBag.Classes = await _context.Classes
                .OrderBy(c => c.CodeClasse)
                .Select(c => new
                {
                    CodeClasse = c.CodeClasse,
                    DisplayText = $"{c.CodeClasse} - {c.Niveau} "
                })
                .ToListAsync();

            return View(etudiant);
        }

        // GET: Etudiant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(m => m.CodeEtudiant == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // GET: Etudiant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(m => m.CodeEtudiant == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant != null)
            {
                _context.Etudiants.Remove(etudiant);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EtudiantExists(string id)
        {
            return _context.Etudiants.Any(e => e.CodeEtudiant == id);
        }
    }
} 