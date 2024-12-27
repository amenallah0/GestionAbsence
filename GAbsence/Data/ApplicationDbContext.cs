using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GAbsence.Models;

namespace GAbsence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Classe> Classes { get; set; }
        public DbSet<Matiere> Matieres { get; set; } 
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<FicheAbsence> FicheAbsences { get; set; }
        public DbSet<FicheAbsenceSeance> FicheAbsenceSeances { get; set; }
        public DbSet<LigneFicheAbsence> LigneFicheAbsences { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des clés primaires
            modelBuilder.Entity<Classe>().HasKey(c => c.CodeClasse);
            modelBuilder.Entity<Matiere>().HasKey(m => m.CodeMatiere);
            modelBuilder.Entity<Seance>().HasKey(s => s.CodeSeance);
            modelBuilder.Entity<Groupe>().HasKey(g => g.CodeGroupe);
            modelBuilder.Entity<FicheAbsence>().HasKey(f => f.CodeFicheAbsence);
            modelBuilder.Entity<FicheAbsenceSeance>().HasKey(fas => new { fas.CodeFicheAbsence, fas.CodeSeance });
            modelBuilder.Entity<LigneFicheAbsence>().HasKey(l => new { l.CodeFicheAbsence, l.CodeEtudiant });
            modelBuilder.Entity<Enseignant>().HasKey(e => e.CodeEnseignant);
            modelBuilder.Entity<Etudiant>().HasKey(e => e.CodeEtudiant);
            modelBuilder.Entity<Departement>().HasKey(d => d.CodeDepartement);
            modelBuilder.Entity<Grade>().HasKey(g => g.CodeGrade);

            modelBuilder.Entity<Enseignant>(entity =>
            {
                entity.ToTable("Enseignants");
                
                entity.HasKey(e => e.CodeEnseignant);

                entity.Property(e => e.CodeDepartement).IsRequired();
                entity.Property(e => e.CodeGrade).IsRequired();

                entity.HasOne(d => d.Departement)
                    .WithMany(p => p.Enseignants)
                    .HasForeignKey(d => d.CodeDepartement)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Enseignants)
                    .HasForeignKey(d => d.CodeGrade)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grades");
                entity.HasKey(e => e.CodeGrade);
            });

            modelBuilder.Entity<Departement>(entity =>
            {
                entity.ToTable("Departements");
                entity.HasKey(e => e.CodeDepartement);
            });

            // Données initiales pour les départements
            modelBuilder.Entity<Departement>().HasData(
                new Departement { CodeDepartement = "qcscsq", NomDepartement = "Département Test" }
            );

            // Données initiales pour les grades
            modelBuilder.Entity<Grade>().HasData(
                new Grade { CodeGrade = "cqs", NomGrade = "Grade Test" }
            );

            // Configuration des relations
            modelBuilder.Entity<Etudiant>(entity =>
            {
                entity.HasKey(e => e.CodeEtudiant);
                
                entity.HasOne(d => d.Classe)
                    .WithMany(p => p.Etudiants)
                    .HasForeignKey(d => d.CodeClasse)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Données initiales pour les classes
            modelBuilder.Entity<Classe>().HasData(
                new Classe { CodeClasse = "DSI31", NomClasse = "DSI 3.1" },
                new Classe { CodeClasse = "DSI32", NomClasse = "DSI 3.2" }
            );
        }
    }
} 