using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GAbsence.Models;
using GAbsence.Data;
using Microsoft.AspNetCore.Authorization;

namespace GAbsence.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Statistiques générales
        ViewBag.TotalEtudiants = _context.Etudiants.Count();
        ViewBag.TotalEnseignants = _context.Enseignants.Count();
        ViewBag.AbsencesMois = _context.Absences
            .Count(a => a.Date.Month == DateTime.Now.Month);
        ViewBag.AbsencesNonJustifiees = _context.Absences
            .Count(a => !a.EstJustifiee);
        ViewBag.AbsencesJustifiees = _context.Absences
            .Count(a => a.EstJustifiee);

        // Données pour le graphique par département
        var absencesByDep = _context.Absences
            .Include(a => a.Etudiant)
                .ThenInclude(e => e.Classe)
                    .ThenInclude(c => c.Filiere)
            .GroupBy(a => a.Etudiant.Classe.Filiere.NomFiliere)
            .Select(g => new { Departement = g.Key, Count = g.Count() })
            .ToList();

        ViewBag.DepartementLabels = absencesByDep.Select(a => a.Departement).ToList();
        ViewBag.DepartementData = absencesByDep.Select(a => a.Count).ToList();

        // Dernières absences
        ViewBag.DernieresAbsences = _context.Absences
            .Include(a => a.Etudiant)
            .Include(a => a.Matiere)
            .Include(a => a.Enseignant)
            .OrderByDescending(a => a.Date)
            .Take(5)
            .Select(a => new
            {
                NomEtudiant = $"{a.Etudiant.Nom} {a.Etudiant.Prenom}",
                Date = a.Date,
                Matiere = a.Matiere.Libelle,
                NomEnseignant = $"{a.Enseignant.Nom} {a.Enseignant.Prenom}",
                EstJustifiee = a.EstJustifiee
            })
            .ToList();

        return View();
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
}
