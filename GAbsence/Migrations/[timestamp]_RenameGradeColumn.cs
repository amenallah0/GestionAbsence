using Microsoft.EntityFrameworkCore.Migrations;

public partial class RenameGradeColumn : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "NomGrade",
            table: "Grades",
            newName: "Libelle");

        // Mettre à jour les données existantes si nécessaire
        migrationBuilder.UpdateData(
            table: "Grades",
            keyColumn: "CodeGrade",
            keyValue: "PR",
            column: "Libelle",
            value: "Professeur");

        migrationBuilder.UpdateData(
            table: "Grades",
            keyColumn: "CodeGrade",
            keyValue: "MCF",
            column: "Libelle",
            value: "Maître de conférences");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Libelle",
            table: "Grades",
            newName: "NomGrade");
    }
} 