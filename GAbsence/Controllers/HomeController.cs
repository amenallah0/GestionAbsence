using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GAbsence.Models;
using GAbsence.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GAbsence.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            // Statistiques des absences
            ViewBag.TotalAbsences = await _context.Absences.CountAsync();
            ViewBag.AbsencesJustifiees = await _context.Absences.CountAsync(a => a.EstJustifiee);
            ViewBag.AbsencesNonJustifiees = await _context.Absences.CountAsync(a => !a.EstJustifiee);

            // Statistiques générales
            ViewBag.NombreEtudiants = await _context.Etudiants.CountAsync();
            ViewBag.NombreEnseignants = await _context.Enseignants.CountAsync();
            ViewBag.NombreClasses = await _context.Classes.CountAsync();
            ViewBag.NombreDepartements = await _context.Departements.CountAsync();
            ViewBag.NombreFilieres = await _context.Filieres.CountAsync();
            ViewBag.NombreMatieres = await _context.Matieres.CountAsync();

            // Absences par matière
            var absencesByMatiere = await _context.Absences
                .Include(a => a.Matiere)
                .Where(a => a.Matiere != null)
                .GroupBy(a => a.Matiere.Libelle)
                .Select(g => new
                {
                    matiere = g.Key ?? "Non spécifié",
                    total = g.Count(),
                    justifiees = g.Count(a => a.EstJustifiee),
                    nonJustifiees = g.Count(a => !a.EstJustifiee)
                })
                .OrderByDescending(x => x.total)
                .Take(5)
                .ToListAsync();

            ViewBag.AbsencesByMatiere = absencesByMatiere;
            ViewBag.TopMatieres = absencesByMatiere;

            var stats = _context.Absences
                .Include(a => a.Etudiant)
                .Where(a => a.Etudiant != null)
                .GroupBy(a => a.Etudiant)
                .Select(g => new
                {
                    Etudiant = g.Key,
                    TotalAbsences = g.Count()
                })
                .ToList();

            return View(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Une erreur s'est produite");
            // Initialiser des valeurs par défaut
            ViewBag.TotalAbsences = 0;
            ViewBag.AbsencesJustifiees = 0;
            ViewBag.AbsencesNonJustifiees = 0;
            ViewBag.NombreEtudiants = 0;
            ViewBag.NombreEnseignants = 0;
            ViewBag.NombreClasses = 0;
            ViewBag.NombreDepartements = 0;
            ViewBag.NombreFilieres = 0;
            ViewBag.NombreMatieres = 0;
            ViewBag.AbsencesByMatiere = new List<object>();
            ViewBag.TopMatieres = new List<object>();

            return View();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult GestionEtudiants()
    {
        return View();
    }

    public IActionResult GestionEnseignants()
    {
        return View();
    }

    public IActionResult GestionMatieres()
    {
        return View();
    }

    public IActionResult GestionAbsences()
    {
        return View();
    }
    
    public IActionResult VerifierDonnees()
    {
        try
        {
            var viewModel = new Dictionary<string, int>();
            
            // Récupérer les comptages
            viewModel["Filieres"] = _context.Filieres.Count();
            viewModel["Classes"] = _context.Classes.Count();
            viewModel["Etudiants"] = _context.Etudiants.Count();
            viewModel["Enseignants"] = _context.Enseignants.Count();
            viewModel["Matieres"] = _context.Matieres.Count();
            viewModel["Departements"] = _context.Departements.Count();
            viewModel["Grades"] = _context.Grades.Count();
            viewModel["Absences"] = _context.Absences.Count();

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des données");
            return View(new Dictionary<string, int>());
        }
    }
}
