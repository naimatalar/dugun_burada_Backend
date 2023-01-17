using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmfsdfs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyGroups_CompanyGroupId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CompanyGroupId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyGroupId",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyGroupId",
                table: "CompanyTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypes_CompanyGroupId",
                table: "CompanyTypes",
                column: "CompanyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypes_CompanyGroups_CompanyGroupId",
                table: "CompanyTypes",
                column: "CompanyGroupId",
                principalTable: "CompanyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypes_CompanyGroups_CompanyGroupId",
                table: "CompanyTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTypes_CompanyGroupId",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "CompanyGroupId",
                table: "CompanyTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyGroupId",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyGroupId",
                table: "Companies",
                column: "CompanyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyGroups_CompanyGroupId",
                table: "Companies",
                column: "CompanyGroupId",
                principalTable: "CompanyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
