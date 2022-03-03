using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Turnos.Migrations
{
    public partial class Medico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    idMedico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    HorarioAtencionDesde = table.Column<DateTime>(nullable: false),
                    HorarioAtencionHasta = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.idMedico);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medico");
        }
    }
}
