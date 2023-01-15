using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmfsdgdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyGroup_CompanyGroupId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyGroup",
                table: "CompanyGroup");

            migrationBuilder.RenameTable(
                name: "CompanyGroup",
                newName: "CompanyGroups");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CompanyGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyGroups",
                table: "CompanyGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyGroups_CompanyGroupId",
                table: "Companies",
                column: "CompanyGroupId",
                principalTable: "CompanyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyGroups_CompanyGroupId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyGroups",
                table: "CompanyGroups");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CompanyGroups");

            migrationBuilder.RenameTable(
                name: "CompanyGroups",
                newName: "CompanyGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyGroup",
                table: "CompanyGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyGroup_CompanyGroupId",
                table: "Companies",
                column: "CompanyGroupId",
                principalTable: "CompanyGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
