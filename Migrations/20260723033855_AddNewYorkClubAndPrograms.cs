using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Picklr.Migrations
{
    /// <inheritdoc />
    public partial class AddNewYorkClubAndPrograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "ClubID", "Description", "Location", "Name" },
                values: new object[] { 3, "An indoor pickleball club located in New York.", "789 Broadway, New York, NY", "Picklr New York" });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "ProgramID", "ClubID", "Description", "Fee", "Friday", "Monday", "Name", "Saturday", "Sunday", "Thursday", "Tuesday", "Wednesday" },
                values: new object[,]
                {
                    { 5, 2, "Weekend social play for all skill levels.", 0.00m, false, false, "Picklr Social", true, false, false, false, false },
                    { 4, 3, "Introduction to pickleball for new players.", 10.00m, true, true, "Picklr 101", true, true, true, true, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "ClubID",
                keyValue: 3);
        }
    }
}
