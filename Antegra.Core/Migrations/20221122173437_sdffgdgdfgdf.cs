using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class sdffgdgdfgdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "PropertySelectLists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectLists_CompanyId",
                table: "PropertySelectLists",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertySelectLists_Companies_CompanyId",
                table: "PropertySelectLists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertySelectLists_Companies_CompanyId",
                table: "PropertySelectLists");

            migrationBuilder.DropIndex(
                name: "IX_PropertySelectLists_CompanyId",
                table: "PropertySelectLists");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PropertySelectLists");
        }
    }
}
