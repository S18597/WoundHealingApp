using Microsoft.EntityFrameworkCore.Migrations;

namespace WoundHealingDb.Migrations
{
    public partial class RepairWoundIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wound_PainLevelId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_PainTypeId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_SurroundingSkinId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundBleedingId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundColorId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundExudateId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundLocationId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundOdorId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundSizeId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundTypeId",
                table: "Wound");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_PainLevelId",
                table: "Wound",
                column: "PainLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_PainTypeId",
                table: "Wound",
                column: "PainTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_SurroundingSkinId",
                table: "Wound",
                column: "SurroundingSkinId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundBleedingId",
                table: "Wound",
                column: "WoundBleedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundColorId",
                table: "Wound",
                column: "WoundColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundExudateId",
                table: "Wound",
                column: "WoundExudateId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundLocationId",
                table: "Wound",
                column: "WoundLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundOdorId",
                table: "Wound",
                column: "WoundOdorId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundSizeId",
                table: "Wound",
                column: "WoundSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundTypeId",
                table: "Wound",
                column: "WoundTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wound_PainLevelId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_PainTypeId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_SurroundingSkinId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundBleedingId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundColorId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundExudateId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundLocationId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundOdorId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundSizeId",
                table: "Wound");

            migrationBuilder.DropIndex(
                name: "IX_Wound_WoundTypeId",
                table: "Wound");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_PainLevelId",
                table: "Wound",
                column: "PainLevelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_PainTypeId",
                table: "Wound",
                column: "PainTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_SurroundingSkinId",
                table: "Wound",
                column: "SurroundingSkinId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundBleedingId",
                table: "Wound",
                column: "WoundBleedingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundColorId",
                table: "Wound",
                column: "WoundColorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundExudateId",
                table: "Wound",
                column: "WoundExudateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundLocationId",
                table: "Wound",
                column: "WoundLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundOdorId",
                table: "Wound",
                column: "WoundOdorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundSizeId",
                table: "Wound",
                column: "WoundSizeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundTypeId",
                table: "Wound",
                column: "WoundTypeId",
                unique: true);
        }
    }
}