using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAbsence.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classe
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes.Include(c => c.Etudiants).ToListAsync();
            return View(classes);
        }

        // GET: Classe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeClasse,NomClasse")] Classe classe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classe);
        }

        // GET: Classe/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes.FindAsync(id);
            if (classe == null)
            {
                return NotFound();
            }
            return View(classe);
        }

        // POST: Classe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeClasse,NomClasse")] Classe classe)
        {
            if (id != classe.CodeClasse)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasseExists(classe.CodeClasse))
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
            return View(classe);
        }

        private bool ClasseExists(string id)
        {
            return _context.Classes.Any(e => e.CodeClasse == id);
        }
    }
} 