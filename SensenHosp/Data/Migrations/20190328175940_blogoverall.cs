using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SensenHosp.Data.Migrations
{
    public partial class blogoverall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_BlogPosts_BlogPostID",
                table: "BlogPostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_BlogTags_BlogTagID",
                table: "BlogPostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "BlogPostTag");

            migrationBuilder.RenameTable(
                name: "BlogPostTag",
                newName: "BlogPostsTags");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostTag_BlogTagID",
                table: "BlogPostsTags",
                newName: "IX_BlogPostsTags_BlogTagID");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostTag_BlogPostID",
                table: "BlogPostsTags",
                newName: "IX_BlogPostsTags_BlogPostID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostsTags",
                table: "BlogPostsTags",
                columns: new[] { "PostId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostsTags_BlogPosts_BlogPostID",
                table: "BlogPostsTags",
                column: "BlogPostID",
                principalTable: "BlogPosts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostsTags_BlogTags_BlogTagID",
                table: "BlogPostsTags",
                column: "BlogTagID",
                principalTable: "BlogTags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostsTags_BlogPosts_BlogPostID",
                table: "BlogPostsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostsTags_BlogTags_BlogTagID",
                table: "BlogPostsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostsTags",
                table: "BlogPostsTags");

            migrationBuilder.RenameTable(
                name: "BlogPostsTags",
                newName: "BlogPostTag");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostsTags_BlogTagID",
                table: "BlogPostTag",
                newName: "IX_BlogPostTag_BlogTagID");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostsTags_BlogPostID",
                table: "BlogPostTag",
                newName: "IX_BlogPostTag_BlogPostID");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "BlogPostTag",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_BlogPosts_BlogPostID",
                table: "BlogPostTag",
                column: "BlogPostID",
                principalTable: "BlogPosts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_BlogTags_BlogTagID",
                table: "BlogPostTag",
                column: "BlogTagID",
                principalTable: "BlogTags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
