using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mymvc.Migrations
{
    /// <inheritdoc />
    public partial class Create_employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee1");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Person11",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Person11",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Person11",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Person11");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Person11");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Person11");

            migrationBuilder.CreateTable(
                name: "Employee1",
                columns: table => new
                {
                    PersonId = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee1", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Employee1_Person11_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person11",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
