using GestionAbscence.Services;
using GestionAbscence.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GestionAbscence.Models;
namespace GestionAbscence.Controllers
{
    public class MatiereController : Controller
    {
        private readonly IMatiereService _matiereService;

        public MatiereController(IMatiereService matiereService)
        {
            _matiereService = matiereService;
        }

        public async Task<IActionResult> Index()
        {
            var matieres = await _matiereService.GetAllMatieres();
            return View(matieres);
        }

        public async Task<IActionResult> Details(string id)
        {
            var matiere = await _matiereService.GetMatiereDetails(id);
            if (matiere == null)
            {
                return NotFound();
            }
            return View(matiere);
        }

        public IActionResult Create()
        {
            return View(new MatiereCreateEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatiereCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _matiereService.CreateMatiere(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var matiere = await _matiereService.GetMatiereForEdit(id);
            if (matiere == null)
            {
                return NotFound();
            }
            return View(matiere);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, MatiereCreateEditViewModel model)
        {
            if (id != model.CodeMatiere)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _matiereService.UpdateMatiere(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Statistics(string id)
        {
            var stats = await _matiereService.GetMatiereStatistics(id);
            if (stats == null)
            {
                return NotFound();
            }
            return View(stats);
        }
    }
}
