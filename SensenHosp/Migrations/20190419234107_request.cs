using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Migrations
{
    public partial class request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "physicianId",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_physicianId",
                table: "Appointments",
                column: "physicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_physician_physicianId",
                table: "Appointments",
                column: "physicianId",
                principalTable: "physician",
                principalColumn: "physicianId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_physician_physicianId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_physicianId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "physicianId",
                table: "Appointments");
        }
    }
}
