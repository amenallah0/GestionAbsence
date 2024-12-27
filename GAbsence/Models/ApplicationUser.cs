using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int GroupeUtilisateur { get; set; } // 1: Admin, 2: Responsable, 3: Enseignant
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
} 