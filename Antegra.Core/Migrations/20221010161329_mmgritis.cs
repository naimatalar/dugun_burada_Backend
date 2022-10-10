using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmgritis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTopics_AspNetUsers_LaboteUserId",
                table: "UserTopics");

            migrationBuilder.DropIndex(
                name: "IX_UserTopics_LaboteUserId",
                table: "UserTopics");

            migrationBuilder.DropColumn(
                name: "LaboteUserId",
                table: "UserTopics");

            migrationBuilder.AddColumn<Guid>(
                name: "UserTopicId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTopicId",
                table: "AspNetUsers",
                column: "UserTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserTopics_UserTopicId",
                table: "AspNetUsers",
                column: "UserTopicId",
                principalTable: "UserTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserTopics_UserTopicId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserTopicId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserTopicId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "LaboteUserId",
                table: "UserTopics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserTopics_LaboteUserId",
                table: "UserTopics",
                column: "LaboteUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTopics_AspNetUsers_LaboteUserId",
                table: "UserTopics",
                column: "LaboteUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
