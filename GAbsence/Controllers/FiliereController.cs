 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAbsence.Models;
using GAbsence.Data;

namespace GAbsence.Controllers
{
    public class FiliereController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FiliereController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filiere
        public async Task<IActionResult> Index()
        {
            var filieres = await _context.Filieres
                .Include(f => f.Classes)
                .ToListAsync();
            return View(filieres);
        }

        // GET: Filiere/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _context.Filieres
                .Include(f => f.Classes)
                .FirstOrDefaultAsync(m => m.CodeFiliere == id);
            
            if (filiere == null)
            {
                return NotFound();
            }

            return View(filiere);
        }

        // GET: Filiere/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filiere/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeFiliere,NomFiliere,Description")] Filiere filiere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filiere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filiere);
        }

        // GET: Filiere/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _context.Filieres.FindAsync(id);
            if (filiere == null)
            {
                return NotFound();
            }
            return View(filiere);
        }

        // POST: Filiere/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeFiliere,NomFiliere,Description")] Filiere filiere)
        {
            if (id != filiere.CodeFiliere)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filiere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiliereExists(filiere.CodeFiliere))
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
            return View(filiere);
        }

        // GET: Filiere/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _context.Filieres
                .FirstOrDefaultAsync(m => m.CodeFiliere == id);
            if (filiere == null)
            {
                return NotFound();
            }

            return View(filiere);
        }

        // POST: Filiere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var filiere = await _context.Filieres.FindAsync(id);
            if (filiere != null)
            {
                _context.Filieres.Remove(filiere);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FiliereExists(string id)
        {
            return _context.Filieres.Any(e => e.CodeFiliere == id);
        }
    }
}