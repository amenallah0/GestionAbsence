using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAbsence.Models;
using GAbsence.Data;
using Microsoft.Extensions.Logging;

namespace GAbsence.Controllers
{
    public class MatiereController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MatiereController> _logger;

        public MatiereController(ApplicationDbContext context, ILogger<MatiereController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Matiere
        public async Task<IActionResult> Index()
        {
            return View(await _context.Matieres.ToListAsync());
        }

        // GET: Matiere/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Matiere/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeMatiere,Libelle,NbreHeures,Coefficient")] Matiere matiere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matiere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matiere);
        }

        // GET: Matiere/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matiere = await _context.Matieres.FindAsync(id);
            if (matiere == null)
            {
                return NotFound();
            }
            return View(matiere);
        }

        // POST: Matiere/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeMatiere,Libelle,NbreHeures,Coefficient")] Matiere matiere)
        {
            if (id != matiere.CodeMatiere)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matiere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatiereExists(matiere.CodeMatiere))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(matiere);
        }

        // GET: Matiere/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matiere = await _context.Matieres.FirstOrDefaultAsync(m => m.CodeMatiere == id);
            if (matiere == null)
            {
                return NotFound();
            }

            return View(matiere);
        }

        // GET: Matiere/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matiere = await _context.Matieres
                .Include(m => m.Absences)
                .Include(m => m.Enseignants)
                .FirstOrDefaultAsync(m => m.CodeMatiere == id);

            if (matiere == null)
            {
                return NotFound();
            }

            return View(matiere);
        }

        // POST: Matiere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var matiere = await _context.Matieres
                    .Include(m => m.Absences)
                    .Include(m => m.Enseignants)
                    .FirstOrDefaultAsync(m => m.CodeMatiere == id);

                if (matiere == null)
                {
                    return NotFound();
                }

                // Vérifier s'il y a des absences liées
                if (matiere.Absences?.Any() == true)
                {
                    ModelState.AddModelError("", "Impossible de supprimer cette matière car elle est liée à des absences.");
                    return View(matiere);
                }

                // Supprimer d'abord les relations avec les enseignants
                if (matiere.Enseignants != null)
                {
                    matiere.Enseignants.Clear();
                    await _context.SaveChangesAsync();
                }

                // Maintenant supprimer la matière
                _context.Matieres.Remove(matiere);
                await _context.SaveChangesAsync();

                TempData["Success"] = "La matière a été supprimée avec succès.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de la suppression de la matière {id}: {ex.Message}");
                ModelState.AddModelError("", "Une erreur s'est produite lors de la suppression de la matière. " +
                                          "Veuillez vérifier qu'elle n'est pas utilisée dans d'autres enregistrements.");
                
                // Recharger la matière pour la vue
                var matiere = await _context.Matieres
                    .Include(m => m.Absences)
                    .Include(m => m.Enseignants)
                    .FirstOrDefaultAsync(m => m.CodeMatiere == id);

                return View(matiere);
            }
        }

        private bool MatiereExists(string id)
        {
            return _context.Matieres.Any(e => e.CodeMatiere == id);
        }
    }
} 