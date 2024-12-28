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
    }
} 