using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SensenHosp.Data.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Albums_AlbumID",
                table: "Media");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Albums_TempId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Albums");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Media",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Albums",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    DonorEmail = table.Column<string>(nullable: true),
                    DonorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Albums_AlbumID",
                table: "Media",
                column: "AlbumID",
                principalTable: "Albums",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Albums_AlbumID",
                table: "Media");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Albums");

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Albums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Albums_TempId",
                table: "Albums",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Albums_AlbumID",
                table: "Media",
                column: "AlbumID",
                principalTable: "Albums",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
