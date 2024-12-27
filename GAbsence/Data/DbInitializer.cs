using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public static class DbInitializer
{
    public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Administrateur", "Responsable", "Enseignant" };
        
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public static async Task InitializeAdmin(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "admin@gestion.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                GroupeUtilisateur = 1,
                Nom = "Admin",
                Prenom = "System"
            };

            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Administrateur");
            }
        }
    }
} 