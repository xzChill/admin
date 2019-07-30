using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace admin.Migrations
{
    public partial class power_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastIP",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    IsBase = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleOperate",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    ModuleId = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleOperate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Power",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: true),
                    ModuleOperateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Power", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "ModuleOperate");

            migrationBuilder.DropTable(
                name: "Power");

            migrationBuilder.DropColumn(
                name: "LastIP",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "User");
        }
    }
}
