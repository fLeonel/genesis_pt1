using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CazuelaChapina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBodegaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BodegaId",
                table: "Productos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bodegas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    BodegaPadreId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bodegas_Bodegas_BodegaPadreId",
                        column: x => x.BodegaPadreId,
                        principalTable: "Bodegas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_BodegaId",
                table: "Productos",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_BodegaPadreId",
                table: "Bodegas",
                column: "BodegaPadreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Bodegas_BodegaId",
                table: "Productos",
                column: "BodegaId",
                principalTable: "Bodegas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Bodegas_BodegaId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Bodegas");

            migrationBuilder.DropIndex(
                name: "IX_Productos_BodegaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "BodegaId",
                table: "Productos");
        }
    }
}
