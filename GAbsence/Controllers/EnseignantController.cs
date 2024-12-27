using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GAbsence.Controllers
{
    public class EnseignantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnseignantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enseignant
        public async Task<IActionResult> Index()
        {
            var enseignants = await _context.Enseignants
                .Include(e => e.Departement)
                .Include(e => e.Grade)
                .ToListAsync();
            return View(enseignants);
        }

        // GET: Enseignant/Create
        public IActionResult Create()
        {
            ViewBag.Departements = _context.Departements.ToList();
            ViewBag.Grades = _context.Grades.ToList();
            return View();
        }

        // POST: Enseignant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeEnseignant,Nom,Prenom,DateRecrutement,Adresse,Mail,Tel,CodeDepartement,CodeGrade")] Enseignant enseignant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Vérifiez que le département et le grade existent
                    var departementExists = await _context.Departements
                        .AnyAsync(d => d.CodeDepartement == enseignant.CodeDepartement);
                    var gradeExists = await _context.Grades
                        .AnyAsync(g => g.CodeGrade == enseignant.CodeGrade);

                    if (!departementExists)
                    {
                        ModelState.AddModelError("CodeDepartement", "Département invalide");
                        goto PrepareView;
                    }

                    if (!gradeExists)
                    {
                        ModelState.AddModelError("CodeGrade", "Grade invalide");
                        goto PrepareView;
                    }

                    // Créez d'abord l'enseignant
                    var newEnseignant = new Enseignant
                    {
                        CodeEnseignant = enseignant.CodeEnseignant,
                        Nom = enseignant.Nom,
                        Prenom = enseignant.Prenom,
                        DateRecrutement = enseignant.DateRecrutement,
                        Adresse = enseignant.Adresse,
                        Mail = enseignant.Mail,
                        Tel = enseignant.Tel,
                        CodeDepartement = enseignant.CodeDepartement,
                        CodeGrade = enseignant.CodeGrade
                    };

                    _context.Add(newEnseignant);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erreur lors de la création : {ex.InnerException?.Message ?? ex.Message}");
            }

        PrepareView:
            ViewBag.Departements = await _context.Departements.ToListAsync();
            ViewBag.Grades = await _context.Grades.ToListAsync();
            return View(enseignant);
        }

        // GET: Enseignant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enseignant = await _context.Enseignants.FindAsync(id);
            if (enseignant == null)
            {
                return NotFound();
            }
            ViewData["CodeDepartement"] = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement", enseignant.CodeDepartement);
            ViewData["CodeGrade"] = new SelectList(_context.Grades, "CodeGrade", "NomGrade", enseignant.CodeGrade);
            return View(enseignant);
        }

        // POST: Enseignant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeEnseignant,Nom,Prenom,DateRecrutement,Adresse,Mail,Tel,CodeDepartement,CodeGrade")] Enseignant enseignant)
        {
            if (id != enseignant.CodeEnseignant)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enseignant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnseignantExists(enseignant.CodeEnseignant))
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
            ViewData["CodeDepartement"] = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement", enseignant.CodeDepartement);
            ViewData["CodeGrade"] = new SelectList(_context.Grades, "CodeGrade", "NomGrade", enseignant.CodeGrade);
            return View(enseignant);
        }

        private bool EnseignantExists(string id)
        {
            return _context.Enseignants.Any(e => e.CodeEnseignant == id);
        }

        private async Task<string> ValidateEnseignant(Enseignant enseignant)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(enseignant.CodeEnseignant))
                messages.Add("Le code enseignant est requis");

            if (string.IsNullOrEmpty(enseignant.CodeDepartement))
                messages.Add("Le code département est requis");
            else if (!await _context.Departements.AnyAsync(d => d.CodeDepartement == enseignant.CodeDepartement))
                messages.Add($"Le département '{enseignant.CodeDepartement}' n'existe pas");

            if (string.IsNullOrEmpty(enseignant.CodeGrade))
                messages.Add("Le code grade est requis");
            else if (!await _context.Grades.AnyAsync(g => g.CodeGrade == enseignant.CodeGrade))
                messages.Add($"Le grade '{enseignant.CodeGrade}' n'existe pas");

            return string.Join(", ", messages);
        }
    }
} 