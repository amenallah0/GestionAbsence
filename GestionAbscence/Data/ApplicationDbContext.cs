using Microsoft.EntityFrameworkCore;
using GestionAbscence.Models;

namespace GestionAbscence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Absence> Absences { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<FicheAbsence> FicheAbsences { get; set; }
        public DbSet<LigneFicheAbsence> LigneFicheAbsences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Etudiant)
                .WithMany(e => e.Absences)
                .HasForeignKey(a => a.EtudiantId);

            modelBuilder.Entity<Absence>()
                .HasOne(a => a.Matiere)
                .WithMany(m => m.Absences)
                .HasForeignKey(a => a.CodeMatiere);
        }
    }
}
