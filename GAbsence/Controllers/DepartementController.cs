using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAbsence.Controllers
{
    public class DepartementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departement
        public async Task<IActionResult> Index()
        {
            var departements = await _context.Departements
                .Include(d => d.Enseignants)
                .ToListAsync();
            return View(departements);
        }

        // GET: Departement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeDepartement,NomDepartement")] Departement departement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departement);
        }

        // GET: Departement/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departement = await _context.Departements.FindAsync(id);
            if (departement == null)
            {
                return NotFound();
            }
            return View(departement);
        }

        // POST: Departement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeDepartement,NomDepartement")] Departement departement)
        {
            if (id != departement.CodeDepartement)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartementExists(departement.CodeDepartement))
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
            return View(departement);
        }

        // GET: Departement/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departement = await _context.Departements
                .Include(d => d.Enseignants)
                .FirstOrDefaultAsync(d => d.CodeDepartement == id);

            if (departement == null)
            {
                return NotFound();
            }

            // Statistiques du département
            ViewBag.NombreEnseignants = departement.Enseignants?.Count ?? 0;

            return View(departement);
        }

        private bool DepartementExists(string id)
        {
            return _context.Departements.Any(e => e.CodeDepartement == id);
        }
    }
} 