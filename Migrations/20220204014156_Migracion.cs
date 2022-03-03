using Microsoft.EntityFrameworkCore.Migrations;

namespace Turnos.Migrations
{
    public partial class Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    idEspecialidad = table.Column<int>(nullable: false)//no se permite dejar en blanco
                        .Annotation("SqlServer:Identity", "1, 1"),//Como es PK , se incrementara de 1 en 1 para asi no haber id similares o iguales
                    descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>//se especifica la PK
                {
                    table.PrimaryKey("PK_Especialidad", x => x.idEspecialidad);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Especialidad");
        }
    }
}
