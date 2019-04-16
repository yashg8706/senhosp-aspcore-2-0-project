using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SensenHosp.Data.Migrations
{
    public partial class feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "FeedbackOnDoctor");

            //migrationBuilder.CreateTable(
            //    name: "physician",
            //    columns: table => new
            //    {
            //        physicianId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        physicianName = table.Column<string>(maxLength: 100, nullable: false),
            //        scheduleStartTime = table.Column<string>(maxLength: 100, nullable: false),
            //        scheduleEndTime = table.Column<string>(maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_physician", x => x.physicianId);
            //    });

            migrationBuilder.CreateTable(
                name: "ReviewOnDoctor",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorName = table.Column<string>(maxLength: 100, nullable: false),
                    Message = table.Column<string>(maxLength: 1000, nullable: false),
                    Reply = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewOnDoctor", x => x.ReviewId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "physician");

            migrationBuilder.DropTable(
                name: "ReviewOnDoctor");

            //migrationBuilder.CreateTable(
            //    name: "FeedbackOnDoctor",
            //    columns: table => new
            //    {
            //        FeedbackID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        DoctorName = table.Column<string>(maxLength: 500, nullable: false),
            //        Feedback = table.Column<string>(maxLength: 1000, nullable: false),
            //        Reply = table.Column<string>(maxLength: 1000, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FeedbackOnDoctor", x => x.FeedbackID);
            //    });
        }
    }
}
