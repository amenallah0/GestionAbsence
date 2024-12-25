using GestionAbscence.Models;
using GestionAbscence.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionAbscence.Models;

namespace GestionAbscence.Controllers
{
    public class EnseignantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnseignantController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var enseignants = await _context.Enseignants
                .Include(e => e.Departement)
                .Include(e => e.Grade)
                .Select(e => new EnseignantViewModel
                {
                    CodeEnseignant = e.CodeEnseignant,
                    Nom = e.Nom,
                    Prenom = e.Prenom,
                    DateRecrutement = e.DateRecrutement,
                    Mail = e.Mail,
                    Tel = e.Tel,
                    CodeDepartement = e.CodeDepartement,
                    CodeGrade = e.CodeGrade
                })
                .ToListAsync();

            return View(enseignants);
        }

        public IActionResult Create()
        {
            ViewBag.Departements = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement");
            ViewBag.Grades = new SelectList(_context.Grades, "CodeGrade", "NomGrade");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnseignantViewModel model)
        {
            if (ModelState.IsValid)
            {
                var enseignant = new Enseignant
                {
                    CodeEnseignant = Guid.NewGuid().ToString(),
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    DateRecrutement = model.DateRecrutement,
                    Adresse = model.Adresse,
                    Mail = model.Mail,
                    Tel = model.Tel,
                    CodeDepartement = model.CodeDepartement,
                    CodeGrade = model.CodeGrade
                };

                _context.Enseignants.Add(enseignant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departements = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement");
            ViewBag.Grades = new SelectList(_context.Grades, "CodeGrade", "NomGrade");
            return View(model);
        }
    }
}
