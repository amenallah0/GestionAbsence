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
            var departements = _context.Departements.ToList();
            var grades = _context.Grades.ToList();

            if (!departements.Any())
            {
                // Initialiser les départements si la table est vide
                var defaultDepartements = new List<Departement>
                {
                    new Departement { CodeDepartement = "INFO", NomDepartement = "Informatique" },
                    new Departement { CodeDepartement = "MATH", NomDepartement = "Mathématiques" }
                };
                _context.Departements.AddRange(defaultDepartements);
                _context.SaveChanges();
                departements = defaultDepartements;
            }

            if (!grades.Any())
            {
                // Initialiser les grades si la table est vide
                var defaultGrades = new List<Grade>
                {
                    new Grade { CodeGrade = "PR", Libelle = "Professeur" },
                    new Grade { CodeGrade = "MCF", Libelle = "Maître de conférences" }
                };
                _context.Grades.AddRange(defaultGrades);
                _context.SaveChanges();
                grades = defaultGrades;
            }

            // Créer les SelectList avec les données vérifiées
            ViewData["CodeDepartement"] = new SelectList(departements, "CodeDepartement", "NomDepartement");
            ViewData["CodeGrade"] = new SelectList(grades, "CodeGrade", "Libelle");

            return View();
        }

        // POST: Enseignant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeEnseignant,Nom,Prenom,Mail,Tel,CodeDepartement,CodeGrade")] Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enseignant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recharger les listes en cas d'erreur
            ViewData["CodeDepartement"] = new SelectList(
                await _context.Departements.ToListAsync(), 
                "CodeDepartement", 
                "NomDepartement", 
                enseignant.CodeDepartement);
            
            ViewData["CodeGrade"] = new SelectList(
                await _context.Grades.ToListAsync(), 
                "CodeGrade", 
                "Libelle", 
                enseignant.CodeGrade);

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
            ViewData["CodeGrade"] = new SelectList(_context.Grades, "CodeGrade", "Libelle", enseignant.CodeGrade);
            return View(enseignant);
        }

        // POST: Enseignant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeEnseignant,Nom,Prenom,Mail,Tel,CodeDepartement,CodeGrade")] Enseignant enseignant)
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
                    return RedirectToAction(nameof(Index));
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
            }

            ViewData["CodeDepartement"] = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement", enseignant.CodeDepartement);
            ViewData["CodeGrade"] = new SelectList(_context.Grades, "CodeGrade", "Libelle", enseignant.CodeGrade);
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

        // GET: Enseignant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enseignant = await _context.Enseignants
                .Include(e => e.Grade)
                .Include(e => e.Departement)
                .FirstOrDefaultAsync(e => e.CodeEnseignant == id);

            if (enseignant == null)
            {
                return NotFound();
            }

            // Récupérer les statistiques des absences supervisées par l'enseignant
            var statistiques = await _context.Absences
                .Where(a => a.CodeEnseignant == id)
                .GroupBy(a => a.Matiere)
                .Select(g => new
                {
                    Matiere = g.Key,
                    TotalAbsences = g.Count(),
                    AbsencesJustifiees = g.Count(a => a.EstJustifiee),
                    AbsencesNonJustifiees = g.Count(a => !a.EstJustifiee)
                })
                .ToListAsync();

            ViewBag.Statistiques = statistiques;
            ViewBag.TotalAbsences = statistiques.Sum(s => s.TotalAbsences);

            return View(enseignant);
        }
    }
} 