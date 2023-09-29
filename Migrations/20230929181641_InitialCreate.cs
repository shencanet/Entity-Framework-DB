using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proyectoef.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "tarea",
                columns: table => new
                {
                    TareaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrioridadTarea = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarea", x => x.TareaID);
                    table.ForeignKey(
                        name: "FK_tarea_categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("1ebc42cb-da81-46c1-93cb-8291f5442e8c"), null, "Actividades personales", 50 },
                    { new Guid("1ebc42cb-da81-46c1-93cb-8291f5442e9c"), null, "Actividades pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "tarea",
                columns: new[] { "TareaID", "CategoriaID", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("1ebc42cb-da81-46c1-93cb-8291f5442e6c"), new Guid("1ebc42cb-da81-46c1-93cb-8291f5442e8c"), null, new DateTime(2023, 9, 29, 20, 16, 41, 274, DateTimeKind.Local).AddTicks(9004), 0, "Terminar serie en netflix" },
                    { new Guid("1ebc42cb-da81-46c1-93cb-8291f5442e7c"), new Guid("1ebc42cb-da81-46c1-93cb-8291f5442e9c"), null, new DateTime(2023, 9, 29, 20, 16, 41, 274, DateTimeKind.Local).AddTicks(8950), 1, "Pago de servicios publicos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tarea_CategoriaID",
                table: "tarea",
                column: "CategoriaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tarea");

            migrationBuilder.DropTable(
                name: "categoria");
        }
    }
}
