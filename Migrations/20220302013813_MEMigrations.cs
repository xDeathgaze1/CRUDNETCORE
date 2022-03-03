using Microsoft.EntityFrameworkCore.Migrations;

namespace Turnos.Migrations
{
    public partial class MEMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicoEspecialidads",
                columns: table => new
                {
                    idMedico = table.Column<int>(nullable: false),
                    idEspecialidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoEspecialidads", x => new { x.idMedico, x.idEspecialidad });
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidads_Especialidad_idEspecialidad",
                        column: x => x.idEspecialidad,
                        principalTable: "Especialidad",
                        principalColumn: "idEspecialidad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidads_Medico_idMedico",
                        column: x => x.idMedico,
                        principalTable: "Medico",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicoEspecialidads_idEspecialidad",
                table: "MedicoEspecialidads",
                column: "idEspecialidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicoEspecialidads");
        }
    }
}
