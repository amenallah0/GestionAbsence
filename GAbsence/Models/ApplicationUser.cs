using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GAbsence.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        public int GroupeUtilisateur { get; set; }

        // Navigation properties
        public virtual Etudiant? Etudiant { get; set; }
        public virtual Enseignant? Enseignant { get; set; }
    }
}