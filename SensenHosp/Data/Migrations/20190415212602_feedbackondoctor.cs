using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Data.Migrations
{
    public partial class feedbackondoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "ReviewOnDoctor",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "ReviewOnDoctor");
        }
    }
}
