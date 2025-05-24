using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuecherVerwaltungEmpty.Migrations
{
    /// <inheritdoc />
    public partial class AddBewertungToBuch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bewertung",
                table: "Buecher",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bewertung",
                table: "Buecher");
        }
    }
}
