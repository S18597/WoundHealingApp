using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WoundHealingDb.Migrations
{
    public partial class UserAndAuthModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(maxLength: 50, nullable: false),
                    Pesel = table.Column<string>(maxLength: 11, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    IsPatient = table.Column<bool>(nullable: false),
                    IsDoctor = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_UserId", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Salt = table.Column<string>(maxLength: 10, nullable: false),
                    Hash = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserId", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Auth_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}