using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmgsir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Companies_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "AspNetRoles",
                newName: "UserTopicId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_CompanyId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_UserTopicId");

            migrationBuilder.CreateTable(
                name: "UserTopics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LaboteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopics_AspNetUsers_LaboteUserId",
                        column: x => x.LaboteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTopics_LaboteUserId",
                table: "UserTopics",
                column: "LaboteUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_UserTopics_UserTopicId",
                table: "AspNetRoles",
                column: "UserTopicId",
                principalTable: "UserTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_UserTopics_UserTopicId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "UserTopics");

            migrationBuilder.RenameColumn(
                name: "UserTopicId",
                table: "AspNetRoles",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_UserTopicId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Companies_CompanyId",
                table: "AspNetRoles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
