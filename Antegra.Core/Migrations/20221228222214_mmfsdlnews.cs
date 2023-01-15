using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmfsdlnews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyGroupId",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyGroupId",
                table: "Companies",
                column: "CompanyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyGroup_CompanyGroupId",
                table: "Companies",
                column: "CompanyGroupId",
                principalTable: "CompanyGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyGroup_CompanyGroupId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyGroup");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CompanyGroupId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyGroupId",
                table: "Companies");
        }
    }
}
