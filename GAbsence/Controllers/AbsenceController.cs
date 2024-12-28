using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GAbsence.Models;
using GAbsence.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAbsence.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AbsenceController> _logger;

        public AbsenceController(ApplicationDbContext context, ILogger<AbsenceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Absence/Create
        public IActionResult Create()
        {
            ViewBag.Etudiants = _context.Etudiants.Select(e => 
                new SelectListItem { 
                    Value = e.CodeEtudiant.ToString(), 
                    Text = $"{e.Nom} {e.Prenom}" 
                }).ToList();
            return View();
        }

        // POST: Absence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Absence absence)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(absence);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Absence créée pour l'étudiant {absence.CodeEtudiant}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erreur lors de la création de l'absence: {ex.Message}");
                    ModelState.AddModelError("", "Une erreur s'est produite lors de l'enregistrement");
                }
            }
            
            ViewBag.Etudiants = _context.Etudiants.Select(e => 
                new SelectListItem { 
                    Value = e.CodeEtudiant.ToString(), 
                    Text = $"{e.Nom} {e.Prenom}" 
                }).ToList();
            return View(absence);
        }

        // GET: Absence/Index
        public async Task<IActionResult> Index()
        {
            var absences = await _context.Absences
                .Include(a => a.Etudiant)
                .ToListAsync();
            return View(absences);
        }

        // GET: Absence/Rapport
        public IActionResult Rapport()
        {
            ViewBag.Etudiants = _context.Etudiants.Select(e => 
                new SelectListItem { 
                    Value = e.CodeEtudiant.ToString(), 
                    Text = $"{e.Nom} {e.Prenom}" 
                }).ToList();
            return View();
        }
    }
} 