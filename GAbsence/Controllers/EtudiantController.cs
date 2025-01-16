using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using GAbsence.Models;

namespace GAbsence.Controllers
{
    public class EtudiantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EtudiantController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public EtudiantController(ApplicationDbContext context, ILogger<EtudiantController> logger,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: Etudiant
        public async Task<IActionResult> Index()
        {
            var etudiants = await _context.Etudiants
                .Include(e => e.Classe)
                .ToListAsync();
            return View(etudiants);
        }

        // GET: Etudiant/Create
        public IActionResult Create()
        {
            ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse");
            return View();
        }

        // POST: Etudiant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeEtudiant,Nom,Prenom,DateNaissance,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Créer le compte utilisateur
                    var user = new ApplicationUser
                    {
                        UserName = etudiant.Mail,
                        Email = etudiant.Mail,
                        Nom = etudiant.Nom,
                        Prenom = etudiant.Prenom,
                        GroupeUtilisateur = UserGroups.Etudiant,
                        EmailConfirmed = true // Pour permettre la connexion immédiate
                    };

                    // Générer le mot de passe selon le format : Etud + CodeEtudiant + !
                    var password = $"Etud{etudiant.CodeEtudiant}!";
                    
                    // Créer l'utilisateur avec le mot de passe
                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        // Ajouter le rôle Etudiant
                        await _userManager.AddToRoleAsync(user, "Etudiant");
                        
                        // Lier l'étudiant au compte utilisateur
                        etudiant.UserId = user.Id;
                        
                        // Créer l'étudiant
                        _context.Add(etudiant);
                        await _context.SaveChangesAsync();

                        // Afficher les informations de connexion
                        TempData["Success"] = $"Étudiant créé avec succès. \nIdentifiants de connexion :\nEmail : {etudiant.Mail}\nMot de passe : {password}";
                        
                        return RedirectToAction(nameof(Index));
                    }
                    
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erreur lors de la création de l'étudiant: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de la création de l'étudiant.");
                }
            }

            ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse");
            return View(etudiant);
        }

        private async Task SendLoginCredentials(string email, string password)
        {
            // Implémenter l'envoi d'email ici
            // Cette méthode devrait envoyer un email à l'étudiant avec ses identifiants
            _logger.LogInformation($"Credentials should be sent to {email}");
        }

        // GET: Etudiant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.CodeEtudiant == id);

            if (etudiant == null)
            {
                return NotFound();
            }

            ViewBag.Classes = await _context.Classes
                .OrderBy(c => c.CodeClasse)
                .Select(c => new
                {
                    CodeClasse = c.CodeClasse,
                    DisplayText = $"{c.CodeClasse} - {c.Niveau} "
                })
                .ToListAsync();

            return View(etudiant);
        }

        // POST: Etudiant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeEtudiant,Nom,Prenom,DateNaissance,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
        {
            if (id != etudiant.CodeEtudiant)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEtudiant = await _context.Etudiants.FindAsync(id);
                    if (existingEtudiant == null)
                    {
                        return NotFound();
                    }

                    // Mettre à jour uniquement les propriétés modifiables
                    existingEtudiant.Nom = etudiant.Nom;
                    existingEtudiant.Prenom = etudiant.Prenom;
                    existingEtudiant.DateNaissance = etudiant.DateNaissance;
                    existingEtudiant.Adresse = etudiant.Adresse;
                    existingEtudiant.Mail = etudiant.Mail;
                    existingEtudiant.Tel = etudiant.Tel;
                    existingEtudiant.CodeClasse = etudiant.CodeClasse;

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Étudiant modifié avec succès: {etudiant.CodeEtudiant}");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!EtudiantExists(etudiant.CodeEtudiant))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError($"Erreur lors de la modification de l'étudiant: {ex.Message}");
                        throw;
                    }
                }
            }
            else
            {
                // Log des erreurs de validation
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Erreur de validation: {modelError.ErrorMessage}");
                }
            }

            ViewBag.Classes = await _context.Classes
                .OrderBy(c => c.CodeClasse)
                .Select(c => new
                {
                    CodeClasse = c.CodeClasse,
                    DisplayText = $"{c.CodeClasse} - {c.Niveau} "
                })
                .ToListAsync();

            return View(etudiant);
        }

        // GET: Etudiant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(m => m.CodeEtudiant == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // GET: Etudiant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(m => m.CodeEtudiant == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant != null)
            {
                _context.Etudiants.Remove(etudiant);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EtudiantExists(string id)
        {
            return _context.Etudiants.Any(e => e.CodeEtudiant == id);
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .Include(e => e.Classe)
                .FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (etudiant == null)
            {
                return NotFound();
            }

            var absences = await _context.Absences
                .Include(a => a.Matiere)
                .Include(a => a.Enseignant)
                .Where(a => a.CodeEtudiant == etudiant.CodeEtudiant)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            ViewBag.Absences = absences;
            return View(etudiant);
        }
    }
} 