using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmhdafsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PropertySelectListValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertySelectListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySelectListValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySelectListValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertySelectListValue_PropertySelectLists_PropertySelectListId",
                        column: x => x.PropertySelectListId,
                        principalTable: "PropertySelectLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectListValue_CompanyId",
                table: "PropertySelectListValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySelectListValue_PropertySelectListId",
                table: "PropertySelectListValue",
                column: "PropertySelectListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertySelectListValue");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "PropertySelectLists",
                type: "uniqueidentifier",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
