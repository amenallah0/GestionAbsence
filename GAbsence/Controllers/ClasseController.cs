using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GAbsence.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClasseController> _logger;

        public ClasseController(ApplicationDbContext context, ILogger<ClasseController> logger)
        {
            _context = context;
            _logger = logger;
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
            _logger.LogInformation("Tentative de création d'une classe");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState invalide");
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Erreur: {modelError.ErrorMessage}");
                }
                return View(classe);
            }

            try
            {
                // Vérifier si le code classe existe déjà
                if (await _context.Classes.AnyAsync(c => c.CodeClasse == classe.CodeClasse))
                {
                    _logger.LogWarning($"Le code classe {classe.CodeClasse} existe déjà");
                    ModelState.AddModelError("CodeClasse", "Ce code de classe existe déjà");
                    return View(classe);
                }

                _context.Add(classe);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Classe créée avec succès: {classe.CodeClasse}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de la création de la classe: {ex.Message}");
                ModelState.AddModelError("", "Une erreur s'est produite lors de la création de la classe");
                return View(classe);
            }
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