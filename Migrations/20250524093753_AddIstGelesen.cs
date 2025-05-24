using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuecherVerwaltungEmpty.Migrations
{
    /// <inheritdoc />
    public partial class AddIstGelesen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IstGelesen",
                table: "Buecher",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Jahr",
                table: "Buecher",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Verlag",
                table: "Buecher",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IstGelesen",
                table: "Buecher");

            migrationBuilder.DropColumn(
                name: "Jahr",
                table: "Buecher");

            migrationBuilder.DropColumn(
                name: "Verlag",
                table: "Buecher");
        }
    }
}
