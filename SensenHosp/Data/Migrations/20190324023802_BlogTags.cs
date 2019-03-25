using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SensenHosp.Data.Migrations
{
    public partial class BlogTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogTags_BlogTagID",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogTagID",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogTagID",
                table: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogPostID = table.Column<int>(nullable: true),
                    BlogTagID = table.Column<int>(nullable: true),
                    PostId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BlogPosts_BlogPostID",
                        column: x => x.BlogPostID,
                        principalTable: "BlogPosts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BlogTags_BlogTagID",
                        column: x => x.BlogTagID,
                        principalTable: "BlogTags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_BlogPostID",
                table: "BlogPostTag",
                column: "BlogPostID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_BlogTagID",
                table: "BlogPostTag",
                column: "BlogTagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.AddColumn<int>(
                name: "BlogTagID",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogTagID",
                table: "BlogPosts",
                column: "BlogTagID");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogTags_BlogTagID",
                table: "BlogPosts",
                column: "BlogTagID",
                principalTable: "BlogTags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
