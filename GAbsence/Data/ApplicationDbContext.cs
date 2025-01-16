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
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Filiere> Filieres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des clés primaires
            modelBuilder.Entity<Classe>().HasKey(c => c.CodeClasse);
            modelBuilder.Entity<Matiere>().HasKey(m => m.CodeMatiere);
            modelBuilder.Entity<Seance>().HasKey(s => s.CodeSeance);
            modelBuilder.Entity<Groupe>().HasKey(g => g.CodeGroupe);
            modelBuilder.Entity<FicheAbsence>().HasKey(f => f.Id);
            modelBuilder.Entity<FicheAbsenceSeance>().HasKey(fas => fas.Id);
            modelBuilder.Entity<LigneFicheAbsence>().HasKey(l => new { l.CodeFiche, l.CodeEtudiant });
            modelBuilder.Entity<Enseignant>().HasKey(e => e.CodeEnseignant);
            modelBuilder.Entity<Etudiant>().HasKey(e => e.CodeEtudiant);
            modelBuilder.Entity<Departement>().HasKey(d => d.CodeDepartement);
            modelBuilder.Entity<Grade>().HasKey(g => g.CodeGrade);
            modelBuilder.Entity<Filiere>().HasKey(f => f.CodeFiliere);

            // Configuration des relations
            modelBuilder.Entity<Classe>()
                .HasOne(c => c.Filiere)
                .WithMany(f => f.Classes)
                .HasForeignKey(c => c.CodeFiliere)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Etudiant>()
                .HasOne(e => e.Classe)
                .WithMany(c => c.Etudiants)
                .HasForeignKey(e => e.CodeClasse)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            modelBuilder.Entity<Filiere>().HasData(
                new Filiere
                {
                    CodeFiliere = "GL",
                    NomFiliere = "Génie Logiciel",
                    Description = "Formation en développement logiciel"
                },
                new Filiere
                {
                    CodeFiliere = "RT",
                    NomFiliere = "Réseaux et Télécommunications",
                    Description = "Formation en réseaux"
                }
            );

            modelBuilder.Entity<Classe>().HasData(
                new Classe
                {
                    CodeClasse = "GL2024",
                    NomClasse = "Génie Logiciel 2024",
                    Niveau = 1,
                    CodeFiliere = "GL"
                },
                new Classe
                {
                    CodeClasse = "RT2024",
                    NomClasse = "Réseaux et Télécoms 2024",
                    Niveau = 1,
                    CodeFiliere = "RT"
                }
            );

            modelBuilder.Entity<Departement>().HasData(
                new Departement
                {
                    CodeDepartement = "INFO",
                    NomDepartement = "Informatique",
                    DateCreation = new DateTime(2024, 1, 1)
                },
                new Departement
                {
                    CodeDepartement = "MATH",
                    NomDepartement = "Mathématiques",
                    DateCreation = new DateTime(2024, 1, 1)
                },
                new Departement
                {
                    CodeDepartement = "PHY",
                    NomDepartement = "Physique",
                    DateCreation = new DateTime(2024, 1, 1)
                }
            );

            // Autres configurations existantes...
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
                new Grade { CodeGrade = "PR", Libelle = "Professeur" },
                new Grade { CodeGrade = "MCF", Libelle = "Maître de conférences" }
            );

            // Configuration de FicheAbsence
            modelBuilder.Entity<FicheAbsence>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasOne(d => d.Etudiant)
                    .WithMany()
                    .HasForeignKey(d => d.CodeEtudiant)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Enseignant)
                    .WithMany()
                    .HasForeignKey(d => d.CodeEnseignant)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuration de FicheAbsenceSeance
            modelBuilder.Entity<FicheAbsenceSeance>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasOne(d => d.FicheAbsence)
                    .WithMany()
                    .HasForeignKey(d => d.FicheAbsenceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Seance)
                    .WithMany()
                    .HasForeignKey(d => d.CodeSeance)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Absence>(entity =>
            {
                entity.HasOne(a => a.Etudiant)
                    .WithMany()
                    .HasForeignKey(a => a.CodeEtudiant)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Enseignant)
                    .WithMany()
                    .HasForeignKey(a => a.CodeEnseignant)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Matiere)
                    .WithMany()
                    .HasForeignKey(a => a.CodeMatiere)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuration de la relation many-to-many entre Matiere et Enseignant
            modelBuilder.Entity<Matiere>()
                .HasMany(m => m.Enseignants)
                .WithMany(e => e.Matieres)
                .UsingEntity<Dictionary<string, object>>(
                    "MatiereEnseignants",
                    j => j
                        .HasOne<Enseignant>()
                        .WithMany()
                        .HasForeignKey("CodeEnseignant")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Matiere>()
                        .WithMany()
                        .HasForeignKey("CodeMatiere")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("CodeEnseignant", "CodeMatiere");
                        j.ToTable("MatiereEnseignants");
                    }
                );

            modelBuilder.Entity<Etudiant>(entity =>
            {
                entity.HasKey(e => e.CodeEtudiant);
                entity.HasOne(e => e.User)
                      .WithOne(u => u.Etudiant)
                      .HasForeignKey<Etudiant>(e => e.UserId);
            });

            modelBuilder.Entity<Enseignant>(entity =>
            {
                entity.HasKey(e => e.CodeEnseignant);
                entity.HasOne(e => e.User)
                      .WithOne(u => u.Enseignant)
                      .HasForeignKey<Enseignant>(e => e.UserId);
            });
        }
    }
} 