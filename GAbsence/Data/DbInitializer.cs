using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GAbsence.Data;
using Microsoft.EntityFrameworkCore;
using GAbsence.Models;
public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Initialiser les rôles
        await InitializeRoles(roleManager);
        
        // Initialiser l'admin
        await InitializeAdmin(userManager);

        // Créer des utilisateurs de test si nécessaire
        if (!await userManager.Users.AnyAsync())
        {
            // Admin
            var adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                Nom = "Admin",
                Prenom = "System",
                EmailConfirmed = true,
                GroupeUtilisateur = UserGroups.Admin
            };
            await CreateUser(userManager, adminUser, "Admin123!", "Admin");

            // Enseignant
            var enseignantUser = new ApplicationUser
            {
                UserName = "enseignant@example.com",
                Email = "enseignant@example.com",
                Nom = "Dupont",
                Prenom = "Jean",
                EmailConfirmed = true,
                GroupeUtilisateur = UserGroups.Enseignant
            };
            await CreateUser(userManager, enseignantUser, "Enseignant123!", "Enseignant");

            // Étudiant
            var etudiantUser = new ApplicationUser
            {
                UserName = "etudiant@example.com",
                Email = "etudiant@example.com",
                Nom = "Martin",
                Prenom = "Sophie",
                EmailConfirmed = true,
                GroupeUtilisateur = UserGroups.Etudiant
            };
            await CreateUser(userManager, etudiantUser, "Etudiant123!", "Etudiant");
        }
    }

    public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "Enseignant", "Etudiant" };

        foreach (string role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public static async Task InitializeAdmin(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Nom = "Admin",
                Prenom = "System",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

    private static async Task CreateUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string password, string role)
    {
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
} 