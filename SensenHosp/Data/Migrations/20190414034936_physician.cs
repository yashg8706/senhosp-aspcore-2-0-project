using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Data.Migrations
{
    public partial class physician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "physician",
                columns: table => new
                {
                    physicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    physicianName = table.Column<string>(maxLength: 100, nullable: false),
                    scheduleStartTime = table.Column<string>(maxLength: 100, nullable: false),
                    scheduleEndTime = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_physician", x => x.physicianId);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "physician");

        }
    }
}
