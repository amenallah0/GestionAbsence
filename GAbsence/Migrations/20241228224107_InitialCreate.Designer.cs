﻿// <auto-generated />
using System;
using GAbsence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GAbsence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241228224107_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("GroupeUtilisateur")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GAbsence.Models.Absence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodeEnseignant")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeEtudiant")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeMatiere")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreneauHoraire")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EstJustifiee")
                        .HasColumnType("bit");

                    b.Property<string>("Justification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatiereCodeMatiere")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CodeEnseignant");

                    b.HasIndex("CodeEtudiant");

                    b.HasIndex("CodeMatiere");

                    b.HasIndex("MatiereCodeMatiere");

                    b.ToTable("Absences");
                });

            modelBuilder.Entity("GAbsence.Models.Classe", b =>
                {
                    b.Property<string>("CodeClasse")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeFiliere")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartementCodeDepartement")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Niveau")
                        .HasColumnType("int");

                    b.Property<string>("NomClasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeClasse");

                    b.HasIndex("CodeFiliere");

                    b.HasIndex("DepartementCodeDepartement");

                    b.ToTable("Classes");

                    b.HasData(
                        new
                        {
                            CodeClasse = "GL2024",
                            CodeFiliere = "GL",
                            Niveau = 1,
                            NomClasse = "Génie Logiciel 2024"
                        },
                        new
                        {
                            CodeClasse = "RT2024",
                            CodeFiliere = "RT",
                            Niveau = 1,
                            NomClasse = "Réseaux et Télécoms 2024"
                        });
                });

            modelBuilder.Entity("GAbsence.Models.Departement", b =>
                {
                    b.Property<string>("CodeDepartement")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomDepartement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeDepartement");

                    b.ToTable("Departements", (string)null);

                    b.HasData(
                        new
                        {
                            CodeDepartement = "INFO",
                            DateCreation = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NomDepartement = "Informatique"
                        },
                        new
                        {
                            CodeDepartement = "qcscsq",
                            NomDepartement = "Département Test"
                        });
                });

            modelBuilder.Entity("GAbsence.Models.Enseignant", b =>
                {
                    b.Property<string>("CodeEnseignant")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeDepartement")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeGrade")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateRecrutement")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeEnseignant");

                    b.HasIndex("CodeDepartement");

                    b.HasIndex("CodeGrade");

                    b.ToTable("Enseignants", (string)null);
                });

            modelBuilder.Entity("GAbsence.Models.Etudiant", b =>
                {
                    b.Property<string>("CodeEtudiant")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeClasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeEtudiant");

                    b.HasIndex("CodeClasse");

                    b.ToTable("Etudiants");
                });

            modelBuilder.Entity("GAbsence.Models.FicheAbsence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodeEnseignant")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeEtudiant")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreneauHoraire")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EstJustifiee")
                        .HasColumnType("bit");

                    b.Property<string>("Justification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matiere")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CodeEnseignant");

                    b.HasIndex("CodeEtudiant");

                    b.ToTable("FicheAbsences");
                });

            modelBuilder.Entity("GAbsence.Models.FicheAbsenceSeance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodeSeance")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FicheAbsenceId")
                        .HasColumnType("int");

                    b.Property<string>("SeanceCodeSeance")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CodeSeance");

                    b.HasIndex("FicheAbsenceId");

                    b.HasIndex("SeanceCodeSeance");

                    b.ToTable("FicheAbsenceSeances");
                });

            modelBuilder.Entity("GAbsence.Models.Filiere", b =>
                {
                    b.Property<string>("CodeFiliere")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomFiliere")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeFiliere");

                    b.ToTable("Filieres");

                    b.HasData(
                        new
                        {
                            CodeFiliere = "GL",
                            Description = "Formation en développement logiciel",
                            NomFiliere = "Génie Logiciel"
                        },
                        new
                        {
                            CodeFiliere = "RT",
                            Description = "Formation en réseaux",
                            NomFiliere = "Réseaux et Télécommunications"
                        });
                });

            modelBuilder.Entity("GAbsence.Models.Grade", b =>
                {
                    b.Property<string>("CodeGrade")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeGrade");

                    b.ToTable("Grades", (string)null);

                    b.HasData(
                        new
                        {
                            CodeGrade = "PR",
                            Libelle = "Professeur"
                        },
                        new
                        {
                            CodeGrade = "MCF",
                            Libelle = "Maître de conférences"
                        });
                });

            modelBuilder.Entity("GAbsence.Models.Groupe", b =>
                {
                    b.Property<string>("CodeGroupe")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClasseCodeClasse")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeClasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomGroupe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodeGroupe");

                    b.HasIndex("ClasseCodeClasse");

                    b.ToTable("Groupes");
                });

            modelBuilder.Entity("GAbsence.Models.LigneFicheAbsence", b =>
                {
                    b.Property<string>("CodeFicheAbsence")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeEtudiant")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EtudiantCodeEtudiant")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("FicheAbsenceSeanceId")
                        .HasColumnType("int");

                    b.HasKey("CodeFicheAbsence", "CodeEtudiant");

                    b.HasIndex("EtudiantCodeEtudiant");

                    b.HasIndex("FicheAbsenceSeanceId");

                    b.ToTable("LigneFicheAbsences");
                });

            modelBuilder.Entity("GAbsence.Models.Matiere", b =>
                {
                    b.Property<string>("CodeMatiere")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Coefficient")
                        .HasColumnType("int");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbreHeures")
                        .HasColumnType("int");

                    b.HasKey("CodeMatiere");

                    b.ToTable("Matieres");
                });

            modelBuilder.Entity("GAbsence.Models.Seance", b =>
                {
                    b.Property<string>("CodeSeance")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeMatiere")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HeureDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HeureFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("MatiereCodeMatiere")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CodeSeance");

                    b.HasIndex("MatiereCodeMatiere");

                    b.ToTable("Seances");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GAbsence.Models.Absence", b =>
                {
                    b.HasOne("GAbsence.Models.Enseignant", "Enseignant")
                        .WithMany()
                        .HasForeignKey("CodeEnseignant")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("CodeEtudiant")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.Matiere", "Matiere")
                        .WithMany()
                        .HasForeignKey("CodeMatiere")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.Matiere", null)
                        .WithMany("Absences")
                        .HasForeignKey("MatiereCodeMatiere");

                    b.Navigation("Enseignant");

                    b.Navigation("Etudiant");

                    b.Navigation("Matiere");
                });

            modelBuilder.Entity("GAbsence.Models.Classe", b =>
                {
                    b.HasOne("GAbsence.Models.Filiere", "Filiere")
                        .WithMany("Classes")
                        .HasForeignKey("CodeFiliere")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.Departement", null)
                        .WithMany("Classes")
                        .HasForeignKey("DepartementCodeDepartement");

                    b.Navigation("Filiere");
                });

            modelBuilder.Entity("GAbsence.Models.Enseignant", b =>
                {
                    b.HasOne("GAbsence.Models.Departement", "Departement")
                        .WithMany("Enseignants")
                        .HasForeignKey("CodeDepartement")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.Grade", "Grade")
                        .WithMany("Enseignants")
                        .HasForeignKey("CodeGrade")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Departement");

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("GAbsence.Models.Etudiant", b =>
                {
                    b.HasOne("GAbsence.Models.Classe", "Classe")
                        .WithMany("Etudiants")
                        .HasForeignKey("CodeClasse")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Classe");
                });

            modelBuilder.Entity("GAbsence.Models.FicheAbsence", b =>
                {
                    b.HasOne("GAbsence.Models.Enseignant", "Enseignant")
                        .WithMany()
                        .HasForeignKey("CodeEnseignant")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("CodeEtudiant")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Enseignant");

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("GAbsence.Models.FicheAbsenceSeance", b =>
                {
                    b.HasOne("GAbsence.Models.Seance", "Seance")
                        .WithMany()
                        .HasForeignKey("CodeSeance")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.FicheAbsence", "FicheAbsence")
                        .WithMany()
                        .HasForeignKey("FicheAbsenceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GAbsence.Models.Seance", null)
                        .WithMany("FicheAbsenceSeances")
                        .HasForeignKey("SeanceCodeSeance");

                    b.Navigation("FicheAbsence");

                    b.Navigation("Seance");
                });

            modelBuilder.Entity("GAbsence.Models.Groupe", b =>
                {
                    b.HasOne("GAbsence.Models.Classe", "Classe")
                        .WithMany()
                        .HasForeignKey("ClasseCodeClasse");

                    b.Navigation("Classe");
                });

            modelBuilder.Entity("GAbsence.Models.LigneFicheAbsence", b =>
                {
                    b.HasOne("GAbsence.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("EtudiantCodeEtudiant");

                    b.HasOne("GAbsence.Models.FicheAbsenceSeance", "FicheAbsenceSeance")
                        .WithMany("LignesFicheAbsence")
                        .HasForeignKey("FicheAbsenceSeanceId");

                    b.Navigation("Etudiant");

                    b.Navigation("FicheAbsenceSeance");
                });

            modelBuilder.Entity("GAbsence.Models.Seance", b =>
                {
                    b.HasOne("GAbsence.Models.Matiere", "Matiere")
                        .WithMany()
                        .HasForeignKey("MatiereCodeMatiere");

                    b.Navigation("Matiere");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GAbsence.Models.Classe", b =>
                {
                    b.Navigation("Etudiants");
                });

            modelBuilder.Entity("GAbsence.Models.Departement", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Enseignants");
                });

            modelBuilder.Entity("GAbsence.Models.FicheAbsenceSeance", b =>
                {
                    b.Navigation("LignesFicheAbsence");
                });

            modelBuilder.Entity("GAbsence.Models.Filiere", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("GAbsence.Models.Grade", b =>
                {
                    b.Navigation("Enseignants");
                });

            modelBuilder.Entity("GAbsence.Models.Matiere", b =>
                {
                    b.Navigation("Absences");
                });

            modelBuilder.Entity("GAbsence.Models.Seance", b =>
                {
                    b.Navigation("FicheAbsenceSeances");
                });
#pragma warning restore 612, 618
        }
    }
}