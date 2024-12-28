using Microsoft.EntityFrameworkCore.Migrations;
using System;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Matieres",
            columns: table => new
            {
                CodeMatiere = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Matieres", x => x.CodeMatiere);
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
                Justification = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Absences", x => x.Id);
                table.ForeignKey(
                    name: "FK_Absences_Etudiants_CodeEtudiant",
                    column: x => x.CodeEtudiant,
                    principalTable: "Etudiants",
                    principalColumn: "CodeEtudiant",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Absences_Matieres_CodeMatiere",
                    column: x => x.CodeMatiere,
                    principalTable: "Matieres",
                    principalColumn: "CodeMatiere",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Absences_Enseignants_CodeEnseignant",
                    column: x => x.CodeEnseignant,
                    principalTable: "Enseignants",
                    principalColumn: "CodeEnseignant",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Absences_CodeEtudiant",
            table: "Absences",
            column: "CodeEtudiant");

        migrationBuilder.CreateIndex(
            name: "IX_Absences_CodeMatiere",
            table: "Absences",
            column: "CodeMatiere");

        migrationBuilder.CreateIndex(
            name: "IX_Absences_CodeEnseignant",
            table: "Absences",
            column: "CodeEnseignant");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Absences");
        migrationBuilder.DropTable(name: "Matieres");
    }
} 