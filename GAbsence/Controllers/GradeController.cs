using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GAbsence.Controllers
{
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GradeController> _logger;

        public GradeController(ApplicationDbContext context, ILogger<GradeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            var grades = await _context.Grades
                .Include(g => g.Enseignants)
                .ToListAsync();
            return View(grades);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeGrade,Libelle")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grade/Edit/MCF
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Enseignants)
                .FirstOrDefaultAsync(g => g.CodeGrade == id);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Edit/MCF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeGrade,Libelle")] Grade grade)
        {
            if (id != grade.CodeGrade)
            {
                return NotFound();
            }

            try
            {
                var existingGrade = await _context.Grades
                    .Include(g => g.Enseignants)
                    .FirstOrDefaultAsync(g => g.CodeGrade == id);

                if (existingGrade == null)
                {
                    return NotFound();
                }

                // Mettre à jour uniquement le libellé
                existingGrade.Libelle = grade.Libelle;

                await _context.SaveChangesAsync();
                _logger.LogInformation($"Grade {id} mis à jour avec succès");
                TempData["Success"] = "Le grade a été modifié avec succès.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de la modification du grade {id}: {ex.Message}");
                ModelState.AddModelError("", "Une erreur est survenue lors de la modification du grade.");
            }

            return View(grade);
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Enseignants)
                .FirstOrDefaultAsync(g => g.CodeGrade == id);

            if (grade == null)
            {
                return NotFound();
            }

            // Statistiques du grade
            ViewBag.NombreEnseignants = grade.Enseignants?.Count ?? 0;

            return View(grade);
        }

        private bool GradeExists(string id)
        {
            return _context.Grades.Any(e => e.CodeGrade == id);
        }
    }
} 