using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class mmhdafsdfgdghgdfsg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertySelectListValue_PropertySelectLists_PropertySelectListId",
                table: "PropertySelectListValue");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertySelectListId",
                table: "PropertySelectListValue",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "PropertySelectListValue",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PropertySelectListValue",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "PropertySelectListValue",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertySelectListValue_PropertySelectLists_PropertySelectListId",
                table: "PropertySelectListValue",
                column: "PropertySelectListId",
                principalTable: "PropertySelectLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertySelectListValue_PropertySelectLists_PropertySelectListId",
                table: "PropertySelectListValue");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "PropertySelectListValue");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PropertySelectListValue");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "PropertySelectListValue");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertySelectListId",
                table: "PropertySelectListValue",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertySelectListValue_PropertySelectLists_PropertySelectListId",
                table: "PropertySelectListValue",
                column: "PropertySelectListId",
                principalTable: "PropertySelectLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
