using GestionAbscence.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionAbscence.Models;
namespace GestionAbscence.Controllers
{
    public class DepartementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departements = await _context.Departements
                .Include(d => d.Enseignants)
                .Include(d => d.Classes)
                .Select(d => new DepartementViewModel
                {
                    CodeDepartement = d.CodeDepartement,
                    NomDepartement = d.NomDepartement,
                    Enseignants = d.Enseignants.Select(e => new EnseignantViewModel
                    {
                        CodeEnseignant = e.CodeEnseignant,
                        Nom = e.Nom,
                        Prenom = e.Prenom
                    }).ToList(),
                    Classes = d.Classes.Select(c => new ClasseViewModel
                    {
                        CodeClasse = c.CodeClasse,
                        NomClasse = c.NomClasse
                    }).ToList() // Initialisation de Classes
                })
                .ToListAsync();

            return View(departements);
        }
    }
}
