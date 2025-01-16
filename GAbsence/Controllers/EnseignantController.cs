using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using GAbsence.Models;

namespace GAbsence.Controllers
{
    public class EnseignantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EnseignantController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnseignantController(
            ApplicationDbContext context, 
            ILogger<EnseignantController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: Enseignant
        public async Task<IActionResult> Index()
        {
            var enseignants = await _context.Enseignants
                .Include(e => e.Departement)
                .Include(e => e.Grade)
                .ToListAsync();
            return View(enseignants);
        }

        // GET: Enseignant/Create
        public IActionResult Create()
        {
            ViewBag.Departements = new SelectList(
                _context.Departements.OrderBy(d => d.NomDepartement),
                "CodeDepartement",
                "NomDepartement"
            );

            ViewBag.Grades = new SelectList(
                _context.Grades.OrderBy(g => g.Libelle),
                "CodeGrade",
                "Libelle"
            );

            return View();
        }

        // POST: Enseignant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeEnseignant,Nom,Prenom,Email,Tel,DateRecrutement,Adresse,CodeDepartement,CodeGrade")] Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Créer le compte utilisateur
                    var user = new ApplicationUser
                    {
                        UserName = enseignant.Email,
                        Email = enseignant.Email,
                        Nom = enseignant.Nom,
                        Prenom = enseignant.Prenom,
                        GroupeUtilisateur = UserGroups.Enseignant,
                        EmailConfirmed = true // Pour permettre la connexion immédiate
                    };

                    // Générer le mot de passe selon le format : Ens + CodeEnseignant + !
                    var password = $"Ens{enseignant.CodeEnseignant}!";
                    
                    // Créer l'utilisateur avec le mot de passe
                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        // Ajouter le rôle Enseignant
                        await _userManager.AddToRoleAsync(user, "Enseignant");
                        
                        // Lier l'enseignant au compte utilisateur
                        enseignant.UserId = user.Id;
                        
                        // Créer l'enseignant
                        _context.Add(enseignant);
                        await _context.SaveChangesAsync();

                        // Afficher les informations de connexion
                        TempData["Success"] = $"Enseignant créé avec succès. \nIdentifiants de connexion :\nEmail : {enseignant.Email}\nMot de passe : {password}";
                        
                        return RedirectToAction(nameof(Index));
                    }
                    
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erreur lors de la création de l'enseignant: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de la création de l'enseignant.");
                }
            }

            // En cas d'erreur, préparer les listes déroulantes
            ViewBag.Departements = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement");
            ViewBag.Grades = new SelectList(_context.Grades, "CodeGrade", "Libelle");
            return View(enseignant);
        }

        // GET: Enseignant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enseignant = await _context.Enseignants
                .Include(e => e.Grade)
                .Include(e => e.Departement)
                .FirstOrDefaultAsync(m => m.CodeEnseignant == id);

            if (enseignant == null)
            {
                return NotFound();
            }

            // Charger les listes pour les dropdowns
            ViewBag.Grades = new SelectList(_context.Grades, "CodeGrade", "Libelle", enseignant.CodeGrade);
            ViewBag.Departements = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement", enseignant.CodeDepartement);

            return View(enseignant);
        }

        // POST: Enseignant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeEnseignant,Nom,Prenom,DateRecrutement,Adresse,Mail,Tel,CodeDepartement,CodeGrade")] Enseignant enseignant)
        {
            if (id != enseignant.CodeEnseignant)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enseignant);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnseignantExists(enseignant.CodeEnseignant))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Recharger les listes en cas d'erreur
            ViewBag.Grades = new SelectList(_context.Grades, "CodeGrade", "Libelle", enseignant.CodeGrade);
            ViewBag.Departements = new SelectList(_context.Departements, "CodeDepartement", "NomDepartement", enseignant.CodeDepartement);

            return View(enseignant);
        }

        // GET: Enseignant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enseignant = await _context.Enseignants
                .Include(e => e.Grade)
                .Include(e => e.Departement)
                .FirstOrDefaultAsync(m => m.CodeEnseignant == id);

            if (enseignant == null)
            {
                return NotFound();
            }

            return View(enseignant);
        }

        // POST: Enseignant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var enseignant = await _context.Enseignants.FindAsync(id);
            if (enseignant != null)
            {
                _context.Enseignants.Remove(enseignant);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool EnseignantExists(string id)
        {
            return _context.Enseignants.Any(e => e.CodeEnseignant == id);
        }

        private async Task<string> ValidateEnseignant(Enseignant enseignant)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(enseignant.CodeEnseignant))
                messages.Add("Le code enseignant est requis");

            if (string.IsNullOrEmpty(enseignant.CodeDepartement))
                messages.Add("Le code département est requis");
            else if (!await _context.Departements.AnyAsync(d => d.CodeDepartement == enseignant.CodeDepartement))
                messages.Add($"Le département '{enseignant.CodeDepartement}' n'existe pas");

            if (string.IsNullOrEmpty(enseignant.CodeGrade))
                messages.Add("Le code grade est requis");
            else if (!await _context.Grades.AnyAsync(g => g.CodeGrade == enseignant.CodeGrade))
                messages.Add($"Le grade '{enseignant.CodeGrade}' n'existe pas");

            return string.Join(", ", messages);
        }

        // GET: Enseignant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enseignant = await _context.Enseignants
                .Include(e => e.Grade)
                .Include(e => e.Departement)
                .FirstOrDefaultAsync(e => e.CodeEnseignant == id);

            if (enseignant == null)
            {
                return NotFound();
            }

            // Récupérer les statistiques des absences supervisées par l'enseignant
            var statistiques = await _context.Absences
                .Where(a => a.CodeEnseignant == id)
                .GroupBy(a => a.Matiere)
                .Select(g => new
                {
                    Matiere = g.Key,
                    TotalAbsences = g.Count(),
                    AbsencesJustifiees = g.Count(a => a.EstJustifiee),
                    AbsencesNonJustifiees = g.Count(a => !a.EstJustifiee)
                })
                .ToListAsync();

            ViewBag.Statistiques = statistiques;
            ViewBag.TotalAbsences = statistiques.Sum(s => s.TotalAbsences);

            return View(enseignant);
        }

        public async Task<IActionResult> Dashboard()
        {
            // Récupérer l'utilisateur connecté
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Récupérer l'enseignant associé à l'utilisateur
            var enseignant = await _context.Enseignants
                .Include(e => e.Departement)
                .Include(e => e.Grade)
                .FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (enseignant == null)
            {
                return NotFound();
            }

            // Récupérer les absences enregistrées par l'enseignant
            var absences = await _context.Absences
                .Include(a => a.Etudiant)
                .Include(a => a.Matiere)
                .Where(a => a.CodeEnseignant == enseignant.CodeEnseignant)
                .OrderByDescending(a => a.Date)
                .Take(10)
                .ToListAsync();

            ViewBag.Absences = absences;
            return View(enseignant);
        }
    }
} 