using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmgksdgfghfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Companies_CompanyId",
                table: "ContactUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactUs",
                table: "ContactUs");

            migrationBuilder.RenameTable(
                name: "ContactUs",
                newName: "ContactUses");

            migrationBuilder.RenameIndex(
                name: "IX_ContactUs_CompanyId",
                table: "ContactUses",
                newName: "IX_ContactUses_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactUses",
                table: "ContactUses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUses_Companies_CompanyId",
                table: "ContactUses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUses_Companies_CompanyId",
                table: "ContactUses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactUses",
                table: "ContactUses");

            migrationBuilder.RenameTable(
                name: "ContactUses",
                newName: "ContactUs");

            migrationBuilder.RenameIndex(
                name: "IX_ContactUses_CompanyId",
                table: "ContactUs",
                newName: "IX_ContactUs_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactUs",
                table: "ContactUs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_Companies_CompanyId",
                table: "ContactUs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
