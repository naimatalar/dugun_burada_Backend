using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmgmdsfkgds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedCompanyTypeCompany_Companies_CompanyId",
                table: "SelectedCompanyTypeCompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectedCompanyTypeCompany",
                table: "SelectedCompanyTypeCompany");

            migrationBuilder.RenameTable(
                name: "SelectedCompanyTypeCompany",
                newName: "SelectedCompanyTypeCompanies");

            migrationBuilder.RenameIndex(
                name: "IX_SelectedCompanyTypeCompany_CompanyId",
                table: "SelectedCompanyTypeCompanies",
                newName: "IX_SelectedCompanyTypeCompanies_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectedCompanyTypeCompanies",
                table: "SelectedCompanyTypeCompanies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedCompanyTypeCompanies_Companies_CompanyId",
                table: "SelectedCompanyTypeCompanies",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedCompanyTypeCompanies_Companies_CompanyId",
                table: "SelectedCompanyTypeCompanies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectedCompanyTypeCompanies",
                table: "SelectedCompanyTypeCompanies");

            migrationBuilder.RenameTable(
                name: "SelectedCompanyTypeCompanies",
                newName: "SelectedCompanyTypeCompany");

            migrationBuilder.RenameIndex(
                name: "IX_SelectedCompanyTypeCompanies_CompanyId",
                table: "SelectedCompanyTypeCompany",
                newName: "IX_SelectedCompanyTypeCompany_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectedCompanyTypeCompany",
                table: "SelectedCompanyTypeCompany",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedCompanyTypeCompany_Companies_CompanyId",
                table: "SelectedCompanyTypeCompany",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
