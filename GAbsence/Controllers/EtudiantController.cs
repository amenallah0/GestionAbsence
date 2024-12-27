using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GAbsence.Controllers
{
    public class EtudiantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EtudiantController(ApplicationDbContext context)
        {
            _context = context;
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
            ViewBag.Classes = _context.Classes.ToList();
            return View();
        }

        // POST: Etudiant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeEtudiant,Nom,Prenom,DateNaissance,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                // Vérifier si le code étudiant existe déjà
                if (await _context.Etudiants.AnyAsync(e => e.CodeEtudiant == etudiant.CodeEtudiant))
                {
                    ModelState.AddModelError("CodeEtudiant", "Ce code étudiant existe déjà");
                    return View(etudiant);
                }

                // Vérifier si la classe existe
                if (!await _context.Classes.AnyAsync(c => c.CodeClasse == etudiant.CodeClasse))
                {
                    ModelState.AddModelError("CodeClasse", "Cette classe n'existe pas");
                    ViewBag.Classes = await _context.Classes.ToListAsync();
                    return View(etudiant);
                }

                try
                {
                    _context.Add(etudiant);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Une erreur s'est produite lors de la création de l'étudiant");
                }
            }

            ViewBag.Classes = await _context.Classes.ToListAsync();
            return View(etudiant);
        }

        // GET: Etudiant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null)
            {
                return NotFound();
            }
            ViewData["CodeClasse"] = new SelectList(_context.Classes, "CodeClasse", "NomClasse", etudiant.CodeClasse);
            return View(etudiant);
        }

        // POST: Etudiant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeEtudiant,Nom,Prenom,DateNaissance,CodeClasse,Adresse,Mail,Tel")] Etudiant etudiant)
        {
            if (id != etudiant.CodeEtudiant)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etudiant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtudiantExists(etudiant.CodeEtudiant))
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
            ViewData["CodeClasse"] = new SelectList(_context.Classes, "CodeClasse", "NomClasse", etudiant.CodeClasse);
            return View(etudiant);
        }

        private bool EtudiantExists(string id)
        {
            return _context.Etudiants.Any(e => e.CodeEtudiant == id);
        }
    }
} 