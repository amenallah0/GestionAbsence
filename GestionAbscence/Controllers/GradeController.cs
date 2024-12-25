using GestionAbscence.Services;
using GestionAbscence.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GestionAbscence.Models;
namespace GestionAbscence.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        public async Task<IActionResult> Index()
        {
            var grades = await _gradeService.GetAllGrades();
            return View(grades);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _gradeService.CreateGrade(model);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var grade = await _gradeService.GetGradeById(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, GradeViewModel model)
        {
            if (id != model.CodeGrade)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _gradeService.UpdateGrade(model);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var grade = await _gradeService.GetGradeById(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _gradeService.DeleteGrade(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
