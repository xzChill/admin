using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace admin.Migrations
{
    public partial class user_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTime",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
