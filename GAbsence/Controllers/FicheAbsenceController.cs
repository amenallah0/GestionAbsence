using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAbsence.Controllers
{
    public class FicheAbsenceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FicheAbsenceController> _logger;

        public FicheAbsenceController(ApplicationDbContext context, ILogger<FicheAbsenceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: FicheAbsence
        public async Task<IActionResult> Index()
        {
            var absences = await _context.FicheAbsences
                .Include(f => f.Etudiant)
                .Include(f => f.Enseignant)
                .OrderByDescending(f => f.Date)
                .ToListAsync();
            return View(absences);
        }

        // GET: FicheAbsence/Create
        public IActionResult Create()
        {
            ViewBag.Etudiants = new SelectList(_context.Etudiants
                .OrderBy(e => e.Nom)
                .Select(e => new
                {
                    e.CodeEtudiant,
                    NomComplet = $"{e.Nom} {e.Prenom} ({e.CodeEtudiant})"
                }), "CodeEtudiant", "NomComplet");

            ViewBag.Enseignants = new SelectList(_context.Enseignants
                .OrderBy(e => e.Nom)
                .Select(e => new
                {
                    e.CodeEnseignant,
                    NomComplet = $"{e.Nom} {e.Prenom} ({e.CodeEnseignant})"
                }), "CodeEnseignant", "NomComplet");

            ViewBag.Matieres = new SelectList(_context.Matieres
                .OrderBy(m => m.Libelle)
                .Select(m => new
                {
                    CodeMatiere = m.CodeMatiere,
                    LibelleComplet = $"{m.Libelle} ({m.CodeMatiere})"
                }), "CodeMatiere", "LibelleComplet");

            return View();
        }

        // POST: FicheAbsence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,CreneauHoraire,CodeEtudiant,CodeEnseignant,Matiere,EstJustifiee,Justification")] FicheAbsence ficheAbsence)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(ficheAbsence);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erreur lors de la création de la fiche d'absence: {ex.Message}");
                    ModelState.AddModelError("", "Une erreur s'est produite lors de la création de la fiche d'absence");
                }
            }

            // Recharger les listes en cas d'erreur
            ViewBag.Etudiants = new SelectList(_context.Etudiants
                .OrderBy(e => e.Nom)
                .Select(e => new
                {
                    e.CodeEtudiant,
                    NomComplet = $"{e.Nom} {e.Prenom} ({e.CodeEtudiant})"
                }), "CodeEtudiant", "NomComplet", ficheAbsence.CodeEtudiant);

            ViewBag.Enseignants = new SelectList(_context.Enseignants
                .OrderBy(e => e.Nom)
                .Select(e => new
                {
                    e.CodeEnseignant,
                    NomComplet = $"{e.Nom} {e.Prenom} ({e.CodeEnseignant})"
                }), "CodeEnseignant", "NomComplet", ficheAbsence.CodeEnseignant);

            return View(ficheAbsence);
        }

        // GET: FicheAbsence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficheAbsence = await _context.FicheAbsences
                .Include(f => f.Etudiant)
                .Include(f => f.Enseignant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ficheAbsence == null)
            {
                return NotFound();
            }

            return View(ficheAbsence);
        }

        // GET: FicheAbsence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficheAbsence = await _context.FicheAbsences
                .Include(f => f.Etudiant)
                .Include(f => f.Enseignant)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ficheAbsence == null)
            {
                return NotFound();
            }

            // Synchroniser avec la vue Create
            ViewData["CodeEtudiant"] = new SelectList(_context.Etudiants
                .Select(e => new
                {
                    e.CodeEtudiant,
                    NomComplet = e.Nom + " " + e.Prenom
                }), "CodeEtudiant", "NomComplet");

            ViewData["CodeEnseignant"] = new SelectList(_context.Enseignants
                .Select(e => new
                {
                    e.CodeEnseignant,
                    NomComplet = e.Nom + " " + e.Prenom
                }), "CodeEnseignant", "NomComplet");

            ViewData["CodeMatiere"] = new SelectList(_context.Matieres, "CodeMatiere", "Libelle");

            return View(ficheAbsence);
        }

        // POST: FicheAbsence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CreneauHoraire,CodeEtudiant,CodeEnseignant,Matiere,EstJustifiee,Justification")] FicheAbsence ficheAbsence)
        {
            if (id != ficheAbsence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ficheAbsence);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FicheAbsenceExists(ficheAbsence.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Recharger les listes en cas d'erreur
            ViewData["CodeEtudiant"] = new SelectList(_context.Etudiants
                .Select(e => new
                {
                    e.CodeEtudiant,
                    NomComplet = e.Nom + " " + e.Prenom
                }), "CodeEtudiant", "NomComplet");

            ViewData["CodeEnseignant"] = new SelectList(_context.Enseignants
                .Select(e => new
                {
                    e.CodeEnseignant,
                    NomComplet = e.Nom + " " + e.Prenom
                }), "CodeEnseignant", "NomComplet");

            ViewData["CodeMatiere"] = new SelectList(_context.Matieres, "CodeMatiere", "Libelle");

            return View(ficheAbsence);
        }

        // GET: FicheAbsence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficheAbsence = await _context.FicheAbsences
                .Include(f => f.Etudiant)
                .Include(f => f.Enseignant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ficheAbsence == null)
            {
                return NotFound();
            }

            return View(ficheAbsence);
        }

        // POST: FicheAbsence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ficheAbsence = await _context.FicheAbsences.FindAsync(id);
            if (ficheAbsence != null)
            {
                _context.FicheAbsences.Remove(ficheAbsence);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FicheAbsenceExists(int id)
        {
            return _context.FicheAbsences.Any(e => e.Id == id);
        }
    }
} 