using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Data.Migrations
{
    public partial class done : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Careerid1",
                table: "Applicants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_Careerid1",
                table: "Applicants",
                column: "Careerid1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Careers_Careerid1",
                table: "Applicants",
                column: "Careerid1",
                principalTable: "Careers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Careers_Careerid1",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_Careerid1",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "Careerid1",
                table: "Applicants");
        }
    }
}
