using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GAbsence.Data;

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

        if (!await roleManager.RoleExistsAsync("Enseignant"))
        {
            await roleManager.CreateAsync(new IdentityRole("Enseignant"));
        }

        if (!await roleManager.RoleExistsAsync("Etudiant"))
        {
            await roleManager.CreateAsync(new IdentityRole("Etudiant"));
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
} 