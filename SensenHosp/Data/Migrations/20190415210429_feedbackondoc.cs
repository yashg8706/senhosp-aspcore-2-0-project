using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Data.Migrations
{
    public partial class feedbackondoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "ReviewOnDoctor");

            migrationBuilder.AddColumn<int>(
                name: "physicianId",
                table: "ReviewOnDoctor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewOnDoctor_physicianId",
                table: "ReviewOnDoctor",
                column: "physicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewOnDoctor_physician_physicianId",
                table: "ReviewOnDoctor",
                column: "physicianId",
                principalTable: "physician",
                principalColumn: "physicianId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewOnDoctor_physician_physicianId",
                table: "ReviewOnDoctor");

            migrationBuilder.DropIndex(
                name: "IX_ReviewOnDoctor_physicianId",
                table: "ReviewOnDoctor");

            migrationBuilder.DropColumn(
                name: "physicianId",
                table: "ReviewOnDoctor");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "ReviewOnDoctor",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
