using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Picklr.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailableDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Friday",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Monday",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Saturday",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sunday",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Thursday",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tuesday",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wednesday",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 1,
                columns: new[] { "Friday", "Monday", "Saturday", "Sunday", "Thursday", "Tuesday", "Wednesday" },
                values: new object[] { true, true, false, false, false, false, true });

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 2,
                columns: new[] { "Friday", "Monday", "Saturday", "Sunday", "Thursday", "Tuesday", "Wednesday" },
                values: new object[] { false, false, false, false, true, true, false });

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 3,
                columns: new[] { "Friday", "Monday", "Saturday", "Sunday", "Thursday", "Tuesday", "Wednesday" },
                values: new object[] { false, false, true, true, false, false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Programs");
        }
    }
}
