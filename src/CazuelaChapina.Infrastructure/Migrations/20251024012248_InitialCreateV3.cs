using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CazuelaChapina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalle_Productos_ProductoIngredienteId",
                table: "RecetaDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalle_Recetas_RecetaId",
                table: "RecetaDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecetaDetalle",
                table: "RecetaDetalle");

            migrationBuilder.RenameTable(
                name: "RecetaDetalle",
                newName: "RecetaDetalles");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalle_RecetaId",
                table: "RecetaDetalles",
                newName: "IX_RecetaDetalles_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalle_ProductoIngredienteId",
                table: "RecetaDetalles",
                newName: "IX_RecetaDetalles_ProductoIngredienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecetaDetalles",
                table: "RecetaDetalles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalles_Productos_ProductoIngredienteId",
                table: "RecetaDetalles",
                column: "ProductoIngredienteId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalles_Recetas_RecetaId",
                table: "RecetaDetalles",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalles_Productos_ProductoIngredienteId",
                table: "RecetaDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalles_Recetas_RecetaId",
                table: "RecetaDetalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecetaDetalles",
                table: "RecetaDetalles");

            migrationBuilder.RenameTable(
                name: "RecetaDetalles",
                newName: "RecetaDetalle");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalles_RecetaId",
                table: "RecetaDetalle",
                newName: "IX_RecetaDetalle_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalles_ProductoIngredienteId",
                table: "RecetaDetalle",
                newName: "IX_RecetaDetalle_ProductoIngredienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecetaDetalle",
                table: "RecetaDetalle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalle_Productos_ProductoIngredienteId",
                table: "RecetaDetalle",
                column: "ProductoIngredienteId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalle_Recetas_RecetaId",
                table: "RecetaDetalle",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
