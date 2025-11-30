using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mymvc.Migrations
{
    /// <inheritdoc />
    public partial class deleteCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Person11");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Person11",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
