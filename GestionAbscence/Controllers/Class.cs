using GestionAbscence.Services;
using GestionAbscence.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GestionAbscence.Models;

namespace GestionAbscence.Controllers
{
    public class ClasseController : Controller
    {
        private readonly IClasseService _classeService;

        public ClasseController(IClasseService classeService)
        {
            _classeService = classeService;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _classeService.GetAllClasses();
            return View(classes);
        }

        public async Task<IActionResult> Details(string id)
        {
            var classe = await _classeService.GetClasseDetails(id);
            if (classe == null)
            {
                return NotFound();
            }
            return View(classe);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _classeService.PrepareClasseCreateEditViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClasseCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _classeService.CreateClasse(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            // Recharger les listes en cas d'erreur
            model = await _classeService.PrepareClasseCreateEditViewModel();
            return View(model);
        }

        public async Task<IActionResult> Statistics(string id)
        {
            var stats = await _classeService.GetClasseStatistics(id);
            if (stats == null)
            {
                return NotFound();
            }
            return View(stats);
        }

        public async Task<IActionResult> EmploiDuTemps(string id)
        {
            var emploiDuTemps = await _classeService.GetClasseEmploiDuTemps(id);
            if (emploiDuTemps == null)
            {
                return NotFound();
            }
            return View(emploiDuTemps);
        }
    }
}
