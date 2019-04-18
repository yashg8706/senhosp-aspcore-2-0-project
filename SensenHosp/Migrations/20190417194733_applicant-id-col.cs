using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Migrations
{
    public partial class applicantidcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Applicants",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Applicants",
                newName: "id");
        }
    }
}
