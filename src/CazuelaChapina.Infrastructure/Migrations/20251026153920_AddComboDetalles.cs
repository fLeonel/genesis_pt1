using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CazuelaChapina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddComboDetalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboProducto");

            migrationBuilder.CreateTable(
                name: "ComboDetalles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ComboId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CantidadPorCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboDetalles_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboDetalles_ComboId",
                table: "ComboDetalles",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboDetalles_ProductoId",
                table: "ComboDetalles",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboDetalles");

            migrationBuilder.CreateTable(
                name: "ComboProducto",
                columns: table => new
                {
                    ComboId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductosId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboProducto", x => new { x.ComboId, x.ProductosId });
                    table.ForeignKey(
                        name: "FK_ComboProducto_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboProducto_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboProducto_ProductosId",
                table: "ComboProducto",
                column: "ProductosId");
        }
    }
}
