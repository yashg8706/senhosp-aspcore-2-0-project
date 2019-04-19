using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Migrations
{
    public partial class Payments_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    patientFname = table.Column<string>(maxLength: 255, nullable: false),
                    patientLname = table.Column<string>(maxLength: 255, nullable: false),
                    invoiceId = table.Column<string>(maxLength: 255, nullable: false),
                    invoiceDate = table.Column<DateTime>(nullable: false),
                    amount = table.Column<string>(nullable: false),
                    payeeEmail = table.Column<string>(nullable: true),
                    transactionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
