using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class sdffgdgdfgdfgdfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertySelectLists_Companies_CompanyId",
                table: "PropertySelectLists");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertySelectLists_Companies_CompanyId",
                table: "PropertySelectLists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertySelectLists_Companies_CompanyId",
                table: "PropertySelectLists");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertySelectLists_Companies_CompanyId",
                table: "PropertySelectLists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
