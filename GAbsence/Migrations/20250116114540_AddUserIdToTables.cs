using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GAbsence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupeUtilisateur = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    CodeDepartement = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomDepartement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.CodeDepartement);
                });

            migrationBuilder.CreateTable(
                name: "Filieres",
                columns: table => new
                {
                    CodeFiliere = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomFiliere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filieres", x => x.CodeFiliere);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    CodeGrade = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.CodeGrade);
                });

            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    CodeMatiere = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbreHeures = table.Column<int>(type: "int", nullable: false),
                    Coefficient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.CodeMatiere);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    CodeClasse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomClasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Niveau = table.Column<int>(type: "int", nullable: false),
                    CodeFiliere = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartementCodeDepartement = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.CodeClasse);
                    table.ForeignKey(
                        name: "FK_Classes_Departements_DepartementCodeDepartement",
                        column: x => x.DepartementCodeDepartement,
                        principalTable: "Departements",
                        principalColumn: "CodeDepartement");
                    table.ForeignKey(
                        name: "FK_Classes_Filieres_CodeFiliere",
                        column: x => x.CodeFiliere,
                        principalTable: "Filieres",
                        principalColumn: "CodeFiliere",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enseignants",
                columns: table => new
                {
                    CodeEnseignant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRecrutement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeDepartement = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeGrade = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignants", x => x.CodeEnseignant);
                    table.ForeignKey(
                        name: "FK_Enseignants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Enseignants_Departements_CodeDepartement",
                        column: x => x.CodeDepartement,
                        principalTable: "Departements",
                        principalColumn: "CodeDepartement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enseignants_Grades_CodeGrade",
                        column: x => x.CodeGrade,
                        principalTable: "Grades",
                        principalColumn: "CodeGrade",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seances",
                columns: table => new
                {
                    CodeSeance = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HeureDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeureFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeMatiere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatiereCodeMatiere = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seances", x => x.CodeSeance);
                    table.ForeignKey(
                        name: "FK_Seances_Matieres_MatiereCodeMatiere",
                        column: x => x.MatiereCodeMatiere,
                        principalTable: "Matieres",
                        principalColumn: "CodeMatiere");
                });

            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    CodeEtudiant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeClasse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.CodeEtudiant);
                    table.ForeignKey(
                        name: "FK_Etudiants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Etudiants_Classes_CodeClasse",
                        column: x => x.CodeClasse,
                        principalTable: "Classes",
                        principalColumn: "CodeClasse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groupes",
                columns: table => new
                {
                    CodeGroupe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomGroupe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeClasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClasseCodeClasse = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupes", x => x.CodeGroupe);
                    table.ForeignKey(
                        name: "FK_Groupes_Classes_ClasseCodeClasse",
                        column: x => x.ClasseCodeClasse,
                        principalTable: "Classes",
                        principalColumn: "CodeClasse");
                });

            migrationBuilder.CreateTable(
                name: "MatiereEnseignants",
                columns: table => new
                {
                    CodeEnseignant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeMatiere = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatiereEnseignants", x => new { x.CodeEnseignant, x.CodeMatiere });
                    table.ForeignKey(
                        name: "FK_MatiereEnseignants_Enseignants_CodeEnseignant",
                        column: x => x.CodeEnseignant,
                        principalTable: "Enseignants",
                        principalColumn: "CodeEnseignant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatiereEnseignants_Matieres_CodeMatiere",
                        column: x => x.CodeMatiere,
                        principalTable: "Matieres",
                        principalColumn: "CodeMatiere",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeEtudiant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeMatiere = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeEnseignant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreneauHoraire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstJustifiee = table.Column<bool>(type: "bit", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnseignantCodeEnseignant = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MatiereCodeMatiere = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Absences_Enseignants_CodeEnseignant",
                        column: x => x.CodeEnseignant,
                        principalTable: "Enseignants",
                        principalColumn: "CodeEnseignant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Absences_Enseignants_EnseignantCodeEnseignant",
                        column: x => x.EnseignantCodeEnseignant,
                        principalTable: "Enseignants",
                        principalColumn: "CodeEnseignant");
                    table.ForeignKey(
                        name: "FK_Absences_Etudiants_CodeEtudiant",
                        column: x => x.CodeEtudiant,
                        principalTable: "Etudiants",
                        principalColumn: "CodeEtudiant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Absences_Matieres_CodeMatiere",
                        column: x => x.CodeMatiere,
                        principalTable: "Matieres",
                        principalColumn: "CodeMatiere",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Absences_Matieres_MatiereCodeMatiere",
                        column: x => x.MatiereCodeMatiere,
                        principalTable: "Matieres",
                        principalColumn: "CodeMatiere");
                });

            migrationBuilder.CreateTable(
                name: "FicheAbsences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreneauHoraire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeEtudiant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeEnseignant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Matiere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstJustifiee = table.Column<bool>(type: "bit", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicheAbsences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FicheAbsences_Enseignants_CodeEnseignant",
                        column: x => x.CodeEnseignant,
                        principalTable: "Enseignants",
                        principalColumn: "CodeEnseignant",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FicheAbsences_Etudiants_CodeEtudiant",
                        column: x => x.CodeEtudiant,
                        principalTable: "Etudiants",
                        principalColumn: "CodeEtudiant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FicheAbsenceSeances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FicheAbsenceId = table.Column<int>(type: "int", nullable: false),
                    CodeSeance = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SeanceCodeSeance = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicheAbsenceSeances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FicheAbsenceSeances_FicheAbsences_FicheAbsenceId",
                        column: x => x.FicheAbsenceId,
                        principalTable: "FicheAbsences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FicheAbsenceSeances_Seances_CodeSeance",
                        column: x => x.CodeSeance,
                        principalTable: "Seances",
                        principalColumn: "CodeSeance",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FicheAbsenceSeances_Seances_SeanceCodeSeance",
                        column: x => x.SeanceCodeSeance,
                        principalTable: "Seances",
                        principalColumn: "CodeSeance");
                });

            migrationBuilder.CreateTable(
                name: "LigneFicheAbsences",
                columns: table => new
                {
                    CodeFiche = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeEtudiant = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    EstAbsent = table.Column<bool>(type: "bit", nullable: false),
                    FicheAbsenceId = table.Column<int>(type: "int", nullable: true),
                    FicheAbsenceSeanceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneFicheAbsences", x => new { x.CodeFiche, x.CodeEtudiant });
                    table.ForeignKey(
                        name: "FK_LigneFicheAbsences_Etudiants_CodeEtudiant",
                        column: x => x.CodeEtudiant,
                        principalTable: "Etudiants",
                        principalColumn: "CodeEtudiant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LigneFicheAbsences_FicheAbsenceSeances_FicheAbsenceSeanceId",
                        column: x => x.FicheAbsenceSeanceId,
                        principalTable: "FicheAbsenceSeances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LigneFicheAbsences_FicheAbsences_FicheAbsenceId",
                        column: x => x.FicheAbsenceId,
                        principalTable: "FicheAbsences",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departements",
                columns: new[] { "CodeDepartement", "DateCreation", "NomDepartement" },
                values: new object[,]
                {
                    { "INFO", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Informatique" },
                    { "MATH", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mathématiques" },
                    { "PHY", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Physique" },
                    { "qcscsq", null, "Département Test" }
                });

            migrationBuilder.InsertData(
                table: "Filieres",
                columns: new[] { "CodeFiliere", "Description", "NomFiliere" },
                values: new object[,]
                {
                    { "GL", "Formation en développement logiciel", "Génie Logiciel" },
                    { "RT", "Formation en réseaux", "Réseaux et Télécommunications" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "CodeGrade", "Libelle" },
                values: new object[,]
                {
                    { "MCF", "Maître de conférences" },
                    { "PR", "Professeur" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "CodeClasse", "CodeFiliere", "DepartementCodeDepartement", "Niveau", "NomClasse" },
                values: new object[,]
                {
                    { "GL2024", "GL", null, 1, "Génie Logiciel 2024" },
                    { "RT2024", "RT", null, 1, "Réseaux et Télécoms 2024" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absences_CodeEnseignant",
                table: "Absences",
                column: "CodeEnseignant");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_CodeEtudiant",
                table: "Absences",
                column: "CodeEtudiant");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_CodeMatiere",
                table: "Absences",
                column: "CodeMatiere");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_EnseignantCodeEnseignant",
                table: "Absences",
                column: "EnseignantCodeEnseignant");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_MatiereCodeMatiere",
                table: "Absences",
                column: "MatiereCodeMatiere");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CodeFiliere",
                table: "Classes",
                column: "CodeFiliere");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_DepartementCodeDepartement",
                table: "Classes",
                column: "DepartementCodeDepartement");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignants_CodeDepartement",
                table: "Enseignants",
                column: "CodeDepartement");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignants_CodeGrade",
                table: "Enseignants",
                column: "CodeGrade");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignants_UserId",
                table: "Enseignants",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_CodeClasse",
                table: "Etudiants",
                column: "CodeClasse");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_UserId",
                table: "Etudiants",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FicheAbsences_CodeEnseignant",
                table: "FicheAbsences",
                column: "CodeEnseignant");

            migrationBuilder.CreateIndex(
                name: "IX_FicheAbsences_CodeEtudiant",
                table: "FicheAbsences",
                column: "CodeEtudiant");

            migrationBuilder.CreateIndex(
                name: "IX_FicheAbsenceSeances_CodeSeance",
                table: "FicheAbsenceSeances",
                column: "CodeSeance");

            migrationBuilder.CreateIndex(
                name: "IX_FicheAbsenceSeances_FicheAbsenceId",
                table: "FicheAbsenceSeances",
                column: "FicheAbsenceId");

            migrationBuilder.CreateIndex(
                name: "IX_FicheAbsenceSeances_SeanceCodeSeance",
                table: "FicheAbsenceSeances",
                column: "SeanceCodeSeance");

            migrationBuilder.CreateIndex(
                name: "IX_Groupes_ClasseCodeClasse",
                table: "Groupes",
                column: "ClasseCodeClasse");

            migrationBuilder.CreateIndex(
                name: "IX_LigneFicheAbsences_CodeEtudiant",
                table: "LigneFicheAbsences",
                column: "CodeEtudiant");

            migrationBuilder.CreateIndex(
                name: "IX_LigneFicheAbsences_FicheAbsenceId",
                table: "LigneFicheAbsences",
                column: "FicheAbsenceId");

            migrationBuilder.CreateIndex(
                name: "IX_LigneFicheAbsences_FicheAbsenceSeanceId",
                table: "LigneFicheAbsences",
                column: "FicheAbsenceSeanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MatiereEnseignants_CodeMatiere",
                table: "MatiereEnseignants",
                column: "CodeMatiere");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_MatiereCodeMatiere",
                table: "Seances",
                column: "MatiereCodeMatiere");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Groupes");

            migrationBuilder.DropTable(
                name: "LigneFicheAbsences");

            migrationBuilder.DropTable(
                name: "MatiereEnseignants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FicheAbsenceSeances");

            migrationBuilder.DropTable(
                name: "FicheAbsences");

            migrationBuilder.DropTable(
                name: "Seances");

            migrationBuilder.DropTable(
                name: "Enseignants");

            migrationBuilder.DropTable(
                name: "Etudiants");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Departements");

            migrationBuilder.DropTable(
                name: "Filieres");
        }
    }
}
