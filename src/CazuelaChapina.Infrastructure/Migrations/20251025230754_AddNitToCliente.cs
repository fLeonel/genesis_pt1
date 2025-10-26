using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CazuelaChapina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNitToCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nit",
                table: "Clientes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nit",
                table: "Clientes");
        }
    }
}
