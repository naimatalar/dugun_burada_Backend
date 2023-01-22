using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class fsdgfdsfgdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "CompanyGroups");

            migrationBuilder.AddColumn<string>(
                name: "Desctiption",
                table: "CompanyTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "CompanyTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desctiption",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "CompanyTypes");

            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "CompanyGroups",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
