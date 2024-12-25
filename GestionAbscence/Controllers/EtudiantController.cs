using GestionAbscence.Models;
using GestionAbscence.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionAbscence.Models;

namespace GestionAbscence.Controllers
{
    public class EtudiantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EtudiantController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var etudiants = await _context.Etudiants
                .Include(e => e.Classe)
                .Select(e => new EtudiantViewModel
                {
                    CodeEtudiant = e.CodeEtudiant,
                    Nom = e.Nom,
                    Prenom = e.Prenom,
                    CodeClasse = e.CodeClasse,
                    NumInscription = e.NumInscription,
                    Mail = e.Mail,
                    Tel = e.Tel
                })
                .ToListAsync();

            return View(etudiants);
        }

        public IActionResult Create()
        {
            ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EtudiantViewModel model)
        {
            if (ModelState.IsValid)
            {
                var etudiant = new Etudiant
                {
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    DateNaissance = model.DateNaissance,
                    CodeClasse = model.CodeClasse,
                    NumInscription = model.NumInscription,
                    Mail = model.Mail,
                    Tel = model.Tel
                };

                _context.Etudiants.Add(etudiant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse");
            return View(model);
        }
    }
}
