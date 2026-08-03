using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Picklr.Migrations
{
    /// <inheritdoc />
    public partial class AddClubProgramRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubID",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 1,
                column: "ClubID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 2,
                column: "ClubID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 3,
                column: "ClubID",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ClubID",
                table: "Programs",
                column: "ClubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Clubs_ClubID",
                table: "Programs",
                column: "ClubID",
                principalTable: "Clubs",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Clubs_ClubID",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ClubID",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ClubID",
                table: "Programs");
        }
    }
}
