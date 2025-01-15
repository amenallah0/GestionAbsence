using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GAbsence.Models;
using Microsoft.Extensions.Logging;

namespace GAbsence.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);
                
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    
                    _logger.LogInformation($"L'utilisateur {model.Email} s'est connecté avec succès");
                    
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (roles.Contains("Enseignant"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (roles.Contains("Etudiant"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                
                ModelState.AddModelError(string.Empty, "Tentative de connexion invalide.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Utilisateur déconnecté.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
} 