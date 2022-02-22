using Microsoft.EntityFrameworkCore.Migrations;

namespace WoundHealingDb.Migrations
{
    public partial class AddMedicalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalData",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChronicDeseases = table.Column<string>(maxLength: 1000, nullable: true),
                    Allergies = table.Column<string>(maxLength: 500, nullable: true),
                    Medication = table.Column<string>(maxLength: 500, nullable: true),
                    Pregnancy = table.Column<bool>(nullable: false),
                    Tobacco = table.Column<bool>(nullable: false),
                    Alcohol = table.Column<bool>(nullable: false),
                    Drugs = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalData_UserId", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_MedicalData_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalData");
        }
    }
}