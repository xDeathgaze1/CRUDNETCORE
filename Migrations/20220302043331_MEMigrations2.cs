using Microsoft.EntityFrameworkCore.Migrations;

namespace Turnos.Migrations
{
    public partial class MEMigrations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidads_Especialidad_idEspecialidad",
                table: "MedicoEspecialidads");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidads_Medico_idMedico",
                table: "MedicoEspecialidads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicoEspecialidads",
                table: "MedicoEspecialidads");

            migrationBuilder.RenameTable(
                name: "MedicoEspecialidads",
                newName: "MedicoEspecialidad");

            migrationBuilder.RenameIndex(
                name: "IX_MedicoEspecialidads_idEspecialidad",
                table: "MedicoEspecialidad",
                newName: "IX_MedicoEspecialidad_idEspecialidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicoEspecialidad",
                table: "MedicoEspecialidad",
                columns: new[] { "idMedico", "idEspecialidad" });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidad_Especialidad_idEspecialidad",
                table: "MedicoEspecialidad",
                column: "idEspecialidad",
                principalTable: "Especialidad",
                principalColumn: "idEspecialidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidad_Medico_idMedico",
                table: "MedicoEspecialidad",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidad_Especialidad_idEspecialidad",
                table: "MedicoEspecialidad");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidad_Medico_idMedico",
                table: "MedicoEspecialidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicoEspecialidad",
                table: "MedicoEspecialidad");

            migrationBuilder.RenameTable(
                name: "MedicoEspecialidad",
                newName: "MedicoEspecialidads");

            migrationBuilder.RenameIndex(
                name: "IX_MedicoEspecialidad_idEspecialidad",
                table: "MedicoEspecialidads",
                newName: "IX_MedicoEspecialidads_idEspecialidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicoEspecialidads",
                table: "MedicoEspecialidads",
                columns: new[] { "idMedico", "idEspecialidad" });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidads_Especialidad_idEspecialidad",
                table: "MedicoEspecialidads",
                column: "idEspecialidad",
                principalTable: "Especialidad",
                principalColumn: "idEspecialidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidads_Medico_idMedico",
                table: "MedicoEspecialidads",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
